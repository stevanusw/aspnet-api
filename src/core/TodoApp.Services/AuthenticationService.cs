﻿using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using TodoApp.Contracts.Services;
using TodoApp.Entities;
using TodoApp.Models.Configuration;
using TodoApp.Models.Dtos;
using TodoApp.Models.Exceptions;

namespace TodoApp.Services
{
    internal class AuthenticationService : IAuthenticationService
    {
        private readonly ILogger<AuthenticationService> _logger;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IUserService _userService;
        private readonly JwtConfiguration _jwtConfiguration;

        private User? _user;

        public AuthenticationService(ILogger<AuthenticationService> logger, 
            IMapper mapper, 
            UserManager<User> userManager, 
            IUserService userService,
            JwtConfiguration jwtConfiguration)
        {
            _logger = logger;
            _mapper = mapper;
            _userManager = userManager;
            _userService = userService;
            _jwtConfiguration = jwtConfiguration;
        }

        public async Task<IdentityResult> RegisterUserAsync(UserForRegistrationDto requestDto)
        {
            var user = _mapper.Map<User>(requestDto);
            var result = await _userManager.CreateAsync(user, requestDto.Password!);
            if (result.Succeeded)
            {
                await _userManager.AddToRolesAsync(user, requestDto.Roles!);
            }

            return result;
        }

        public async Task<bool> ValidateUserAsync(UserForLoginDto requestDto)
        {
            _user = await _userManager.FindByNameAsync(requestDto.UserName!);
            var result = _user != null && await _userManager.CheckPasswordAsync(_user, requestDto.Password!);
            if (!result)
            {
                _logger.LogWarning(@"{caller}: Authentication failed. Wrong user name or password.", nameof(ValidateUserAsync));
            }

            return result;
        }

        public async Task<TokenDto> CreateTokenAsync(bool populateExpiration)
        {
            var signingCredentials = GetSigningCredentials();
            var claims = await GetClaims();
            var tokenOptions = GenerateTokenOptions(signingCredentials, claims);

            var refreshToken = GenerateRefreshToken();
            _user!.RefreshToken = refreshToken;

            if (populateExpiration)
            {
                _user!.RefreshTokenExpiryTimeUtc = DateTime.UtcNow.AddDays(7);
            }

            await _userManager.UpdateAsync(_user);

            var accessToken = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            return new TokenDto(accessToken, refreshToken);

            SigningCredentials GetSigningCredentials()
            {
                var secret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfiguration.Secret!));

                return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
            }

            async Task<List<Claim>> GetClaims()
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, _user!.UserName!)
                };

                var roles = await _userManager.GetRolesAsync(_user);
                foreach (var role in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }

                return claims;
            }

            JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
            {
                var tokenOptions = new JwtSecurityToken
                    (
                        issuer: _jwtConfiguration.Issuer,
                        audience: _jwtConfiguration.Audience,
                        claims: claims,
                        expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(_jwtConfiguration.ExpiresMins)),
                        signingCredentials: signingCredentials
                    );

                return tokenOptions;
            }

            string GenerateRefreshToken()
            {
                var randomNumber = new byte[32];

                using (var rng = RandomNumberGenerator.Create())
                {
                    rng.GetBytes(randomNumber);

                    return Convert.ToBase64String(randomNumber);
                }
            }
        }

        public async Task<TokenDto> RefreshTokenAsync(TokenForRefreshDto requestDto)
        {
            var user = await _userService.GetUserByRefreshTokenAsync(requestDto.RefreshToken!, true);
            if (user.RefreshTokenExpiryTimeUtc <= DateTime.UtcNow)
            {
                throw new RefreshTokenBadRequestException();
            }

            _user = user;

            return await CreateTokenAsync(false);
        }
    }
}

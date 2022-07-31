using Microsoft.AspNetCore.Identity;
using TodoApp.Models.Dtos;

namespace TodoApp.Contracts.Services
{
    public interface IAuthenticationService
    {
        Task<IdentityResult> RegisterUserAsync(UserForRegistrationDto requestDto);
        Task<bool> ValidateUserAsync(UserForAuthenticationDto requestDto);
        Task<TokenDto> CreateTokenAsync(bool populateExpiration);
        Task<TokenDto> RefreshTokenAsync(TokenForRefreshDto requestDto);
    }
}

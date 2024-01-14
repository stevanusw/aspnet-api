using Asp.Versioning;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TodoApp.Api.Filters;
using TodoApp.Contracts.Services;
using TodoApp.Models.Dtos;

namespace TodoApp.Api.Controllers.V1
{
    [ApiController]
    [Route("api/v{v:apiVersion}/authentication")]
    [ApiVersion("1.0")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IServiceManager _services;

        public AuthenticationController(IServiceManager services) => _services = services;

        [HttpPost("users")]
        [ServiceFilter(typeof(RequestDtoValidationFilter))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> RegisterUser(UserForRegistrationDto requestDto)
        {
            var result = await _services.Authentication.RegisterUserAsync(requestDto);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }

                return BadRequest(ModelState);
            }

            return StatusCode((int)HttpStatusCode.Created);
        }

        [HttpPost("token")]
        [ServiceFilter(typeof(RequestDtoValidationFilter))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TokenDto))]
        public async Task<IActionResult> Login(UserForLoginDto requestDto)
        {
            if (!await _services.Authentication.ValidateUserAsync(requestDto))
            {
                return Unauthorized();
            }

            var model = await _services.Authentication.CreateTokenAsync(populateExpiration: true);

            return Ok(model);
        }

        [HttpPut("token")]
        [ServiceFilter(typeof(RequestDtoValidationFilter))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TokenDto))]
        public async Task<IActionResult> RefreshToken(TokenForRefreshDto requestDto)
        {
            var model = await _services.Authentication.RefreshTokenAsync(requestDto);

            return Ok(model);
        }
    }
}

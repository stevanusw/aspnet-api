using Microsoft.AspNetCore.Mvc;
using System.Net;
using TodoApp.Api.Filters;
using TodoApp.Contracts.Services;
using TodoApp.Models.Dtos;

namespace TodoApp.Api.Controllers
{
    [ApiController]
    [Route("api/authentication")]
    [ApiVersion("1.0")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IServiceManager _services;

        public AuthenticationController(IServiceManager services) => _services = services;

        [HttpPost("users")]
        [ServiceFilter(typeof(RequestDtoValidationFilter))]
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

        [HttpPost("login")]
        [ServiceFilter(typeof(RequestDtoValidationFilter))]
        public async Task<IActionResult> Login(UserForAuthenticationDto requestDto)
        {
            if (!await _services.Authentication.ValidateUserAsync(requestDto))
            {
                return Unauthorized();
            }

            var model = await _services.Authentication.CreateTokenAsync(populateExpiration: true);

            return Ok(model);
        }
    }
}

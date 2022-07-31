using Microsoft.AspNetCore.Mvc;
using TodoApp.Api.Filters;
using TodoApp.Contracts.Services;
using TodoApp.Models.Dtos;

namespace TodoApp.Api.Controllers
{
    [ApiController]
    [Route("api/token")]
    [ApiVersion("1.0")]
    public class TokenController : ControllerBase
    {
        private readonly IServiceManager _services;

        public TokenController(IServiceManager services)
        {
            _services = services;
        }

        [HttpPut]
        [ServiceFilter(typeof(MediaTypeResolverFilter))]
        public async Task<IActionResult> RefreshToken(TokenForRefreshDto requestDto)
        {
            var model = await _services.Authentication.RefreshTokenAsync(requestDto);

            return Ok(model);
        }
    }
}

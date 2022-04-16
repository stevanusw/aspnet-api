using Microsoft.AspNetCore.Mvc;
using TodoApp.Contracts.Services;

namespace TodoApp.Api.Controllers
{
    [ApiController]
    [Route("api/todos")]
    public class TodosController : ControllerBase
    {
        private readonly IServiceManager _services;

        public TodosController(IServiceManager services)
        {
            _services = services;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var model = await _services.Todo.GetTodosAsync();

            return Ok(model);
        }
    }
}

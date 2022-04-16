using Microsoft.AspNetCore.Mvc;
using TodoApp.Contracts.Services;

namespace TodoApp.Api.Controllers
{
    [ApiController]
    [Route("api/todos/{todoId:int}/tasks")]
    public class TasksController :ControllerBase
    {
        private readonly IServiceManager _services;

        public TasksController(IServiceManager services)
        {
            _services = services;
        }

        [HttpGet]
        public async Task<IActionResult> Get(int todoId)
        {
            var model = await _services.Task.GetTasksAsync(todoId);

            return Ok(model);
        }
    }
}

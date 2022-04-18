using Microsoft.AspNetCore.Mvc;
using TodoApp.Contracts.Services;
using TodoApp.Models;

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
        public async Task<IActionResult> GetTodos()
        {
            var model = await _services.Todo.GetTodosAsync();

            return Ok(model);
        }

        [HttpGet("{id}", Name = nameof(GetTodo))]
        public async Task<IActionResult> GetTodo(int id)
        {
            var model = await _services.Todo.GetTodoAsync(id);

            return Ok(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTodo(TodoForCreationDto todo)
        {
            var model = await _services.Todo.CreateTodoAsync(todo);

            return CreatedAtRoute(nameof(GetTodo),
                new
                {
                    Id = model.Id,
                },
                model);
        }
    }
}

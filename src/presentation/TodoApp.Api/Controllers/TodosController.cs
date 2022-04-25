using Microsoft.AspNetCore.Mvc;
using TodoApp.Contracts.Services;
using TodoApp.Models.Dtos;

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
                    id = model.Id,
                },
                model);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteTodo(int id)
        {
            await _services.Todo.DeleteTodoAsync(id);

            return NoContent();
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateTodo(int id, TodoForUpdateDto todo)
        {
            await _services.Todo.UpdateTodoAsync(id, todo);

            return NoContent();
        }
    }
}

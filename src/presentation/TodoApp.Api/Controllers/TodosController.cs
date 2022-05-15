using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using TodoApp.Api.Extensions;
using TodoApp.Api.Filters;
using TodoApp.Contracts.Services;
using TodoApp.Models.Dtos;
using TodoApp.Models.Parameters;

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
        public async Task<IActionResult> GetTodos([FromQuery]TodoParameters parameters)
        {
            var model = await _services.Todo.GetTodosAsync(parameters);

            return Response.ToPagedOk(model.Dto, model.PageInfo);
        }

        [HttpGet("{id}", Name = nameof(GetTodo))]
        public async Task<IActionResult> GetTodo(int id)
        {
            var model = await _services.Todo.GetTodoAsync(id);

            return Ok(model);
        }

        [HttpPost]
        [ServiceFilter(typeof(RequestDtoValidationFilter))]
        public async Task<IActionResult> CreateTodo(TodoForCreationDto requestDto)
        {
            var model = await _services.Todo.CreateTodoAsync(requestDto);

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
        [ServiceFilter(typeof(RequestDtoValidationFilter))]
        public async Task<IActionResult> UpdateTodo(int id, TodoForUpdateDto requestDto)
        {
            await _services.Todo.UpdateTodoAsync(id, requestDto);

            return NoContent();
        }

        [HttpPatch("{id:int}")]
        public async Task<IActionResult> PartiallyUpdateTodo(int id, JsonPatchDocument<TodoForUpdateDto> requestDto)
        {
            var model = await _services.Todo.GetTodoForPatchAsync(id);

            requestDto.ApplyTo(model.DtoToPatch, ModelState);
            TryValidateModel(model.DtoToPatch);
            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }

            await _services.Todo.UpdateTodoFromPatchAsync(model.DtoToPatch, model.Entity);

            return NoContent();
        }
    }
}

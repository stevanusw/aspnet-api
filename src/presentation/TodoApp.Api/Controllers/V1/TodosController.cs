using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using TodoApp.Api.Extensions;
using TodoApp.Api.Filters;
using TodoApp.Contracts.Services;
using TodoApp.Models.Dtos;
using TodoApp.Models.Parameters;

namespace TodoApp.Api.Controllers.V1
{
    [ApiController]
    [Route("api/v{v:apiVersion}/todos")]
    [ApiVersion("1.0")]
    public class TodosController : ControllerBase
    {
        private readonly IServiceManager _services;

        public TodosController(IServiceManager services) => _services = services;

        [HttpOptions]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetOptions()
        {
            Response.Headers.Add("Allow", "GET, HEAD, OPTIONS, POST, PUT, DELETE, PATCH");

            return Ok();
        }

        /// <summary>
        /// Get the list of all todos
        /// </summary>
        /// <param name="parameters">Query string parameters</param>
        /// <returns>The todos list</returns>
        [HttpGet(Name = nameof(GetTodos))]
        [HttpHead]
        [ServiceFilter(typeof(MediaTypeResolverFilter))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TodoDto>))]
        public async Task<IActionResult> GetTodos([FromQuery] TodoParameters parameters)
        {
            var linkParameters = new LinkParameters(parameters, HttpContext);
            var model = await _services.Todo.GetTodosAsync(linkParameters);
            if (model.Dto.HasLinks)
            {
                return Response.ToPagedOk(model.Dto.LinkedDtos, model.PageInfo);
            }

            return Response.ToPagedOk(model.Dto.ShapedDtos, model.PageInfo);
        }

        [HttpGet("{id}", Name = nameof(GetTodo))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TodoDto))]
        public async Task<IActionResult> GetTodo(int id, [FromQuery] TodoParameters parameters)
        {
            var model = await _services.Todo.GetTodoAsync(id, parameters);

            return Ok(model);
        }

        /// <summary>
        /// Creates a newly created todo
        /// </summary>
        /// <param name="requestDto"></param>
        /// <returns>A newly created todo</returns>
        /// <response code="201">Returns the newly created todo</response>
        /// <response code="400">If the model is null</response>
        /// <response code="422">If the model is invalid</response>
        [HttpPost(Name = nameof(CreateTodo))]
        [ServiceFilter(typeof(RequestDtoValidationFilter))]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(TodoDto))]
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
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteTodo(int id)
        {
            await _services.Todo.DeleteTodoAsync(id);

            return NoContent();
        }

        [HttpPut("{id:int}")]
        [ServiceFilter(typeof(RequestDtoValidationFilter))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateTodo(int id, TodoForUpdateDto requestDto)
        {
            await _services.Todo.UpdateTodoAsync(id, requestDto);

            return NoContent();
        }

        [HttpPatch("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
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

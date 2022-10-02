using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TodoApp.Api.Extensions;
using TodoApp.Api.Filters;
using TodoApp.Contracts.Services;
using TodoApp.Models.Dtos;
using TodoApp.Models.Parameters;

namespace TodoApp.Api.Controllers.V1
{
    [ApiController]
    [Route("api/v{v:apiVersion}/todos/{todoId:int}/tasks")]
    [ApiVersion("1.0")]
    public class TasksController : ControllerBase
    {
        private readonly IServiceManager _services;

        public TasksController(IServiceManager services) => _services = services;

        [HttpOptions]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetOptions()
        {
            Response.Headers.Add("Allow", "GET, HEAD, OPTIONS, POST, PUT, DELETE");

            return Ok();
        }

        [HttpGet]
        [HttpHead]
        [ServiceFilter(typeof(MediaTypeResolverFilter))]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TaskDto>))]
        public async Task<IActionResult> GetTasks(int todoId, [FromQuery] TaskParameters parameters)
        {
            var linkParameters = new LinkParameters(parameters, HttpContext);
            var model = await _services.Task.GetTasksAsync(todoId, linkParameters);
            if (model.Dto.HasLinks)
            {
                return Response.ToPagedOk(model.Dto.LinkedDtos, model.PageInfo);
            }

            return Response.ToPagedOk(model.Dto.ShapedDtos, model.PageInfo);
        }

        [HttpGet("{taskId:int}", Name = nameof(GetTask))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TaskDto))]
        public async Task<IActionResult> GetTask(int todoId, int taskId, [FromQuery] TaskParameters parameters)
        {
            var model = await _services.Task.GetTaskAsync(todoId, taskId, parameters);

            return Ok(model);
        }

        [HttpPost]
        [ServiceFilter(typeof(RequestDtoValidationFilter))]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(TaskDto))]
        public async Task<IActionResult> CreateTask(int todoId, TaskForCreationDto requestDto)
        {
            var model = await _services.Task.CreateTaskAsync(todoId, requestDto);

            return CreatedAtRoute(nameof(GetTask),
                new
                {
                    todoId,
                    taskId = model.Id,
                },
                model);
        }

        [HttpDelete("{taskId:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteTask(int todoId, int taskId)
        {
            await _services.Task.DeleteTaskAsync(todoId, taskId);

            return NoContent();
        }

        [HttpPut("{taskId:int}")]
        [ServiceFilter(typeof(RequestDtoValidationFilter))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateTask(int todoId, int taskId, TaskForUpdateDto requestDto)
        {
            await _services.Task.UpdateTaskAsync(todoId, taskId, requestDto);

            return NoContent();
        }
    }
}

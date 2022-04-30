using Microsoft.AspNetCore.Mvc;
using TodoApp.Contracts.Services;
using TodoApp.Models.Dtos;

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
        public async Task<IActionResult> GetTasks(int todoId)
        {
            var model = await _services.Task.GetTasksAsync(todoId);

            return Ok(model);
        }

        [HttpGet("{taskId:int}", Name = nameof(GetTask))]
        public async Task<IActionResult> GetTask(int todoId, int taskId)
        {
            var model = await _services.Task.GetTaskAsync(todoId, taskId);

            return Ok(model);
        }

        [HttpPost]
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
        public async Task<IActionResult> DeleteTask(int todoId, int taskId)
        {
            await _services.Task.DeleteTaskAsync(todoId, taskId);

            return NoContent();
        }

        [HttpPut("{taskId:int}")]
        public async Task<IActionResult> UpdateTask(int todoId, int taskId, TaskForUpdateDto requestDto)
        {
            await _services.Task.UpdateTaskAsync(todoId, taskId, requestDto);

            return NoContent();
        }
    }
}

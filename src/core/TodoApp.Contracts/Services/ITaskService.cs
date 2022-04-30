using TodoApp.Models.Dtos;

namespace TodoApp.Contracts.Services
{
    public interface ITaskService
    {
        Task<IEnumerable<TaskDto>> GetTasksAsync(int todoId);
        Task<TaskDto> GetTaskAsync(int todoId, int taskId);
        Task<TaskDto> CreateTaskAsync(int todoId, TaskForCreationDto requestDto);
        Task DeleteTaskAsync(int todoId, int taskId);
        Task UpdateTaskAsync(int todoId, int taskId, TaskForUpdateDto requestDto);
    }
}

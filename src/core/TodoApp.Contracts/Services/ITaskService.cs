using TodoApp.Models.Dtos;
using TodoApp.Models.Paging;
using TodoApp.Models.Parameters;

namespace TodoApp.Contracts.Services
{
    public interface ITaskService
    {
        Task<(IEnumerable<TaskDto> Dto, PageInfo PageInfo)> GetTasksAsync(int todoId, TaskParameters parameters);
        Task<TaskDto> GetTaskAsync(int todoId, int taskId);
        Task<TaskDto> CreateTaskAsync(int todoId, TaskForCreationDto requestDto);
        Task DeleteTaskAsync(int todoId, int taskId);
        Task UpdateTaskAsync(int todoId, int taskId, TaskForUpdateDto requestDto);
    }
}

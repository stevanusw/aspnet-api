using TodoApp.Models.Dtos;

namespace TodoApp.Contracts.Services
{
    public interface ITaskService
    {
        Task<IEnumerable<TaskDto>> GetTasksAsync(int todoId);
    }
}

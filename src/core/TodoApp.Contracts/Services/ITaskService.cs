using System.Dynamic;
using TodoApp.Models.Dtos;
using TodoApp.Models.Paging;
using TodoApp.Models.Parameters;

namespace TodoApp.Contracts.Services
{
    public interface ITaskService
    {
        Task<(IEnumerable<ExpandoObject> Dto, PageInfo PageInfo)> GetTasksAsync(int todoId, LinkParameters linkParameters);
        Task<ExpandoObject> GetTaskAsync(int todoId, int taskId, TaskParameters parameters);
        Task<TaskDto> CreateTaskAsync(int todoId, TaskForCreationDto requestDto);
        Task DeleteTaskAsync(int todoId, int taskId);
        Task UpdateTaskAsync(int todoId, int taskId, TaskForUpdateDto requestDto);
    }
}

using Tasks = System.Threading.Tasks;
using TodoApp.Entities;
using TodoApp.Models.Dtos;

namespace TodoApp.Contracts.Services
{
    public interface ITodoService
    {
        Task<IEnumerable<TodoDto>> GetTodosAsync();
        Task<TodoDto> GetTodoAsync(int id);
        Task<TodoDto> CreateTodoAsync(TodoForCreationDto request);
        Tasks.Task DeleteTodoAsync(int id);
        Tasks.Task UpdateTodoAsync(int id, TodoForUpdateDto request);
        Task<(TodoForUpdateDto DtoToPatch, Todo Entity)> GetTodoForPatchAsync(int id);
        Tasks.Task UpdateTodoFromPatchAsync(TodoForUpdateDto request, Todo entity);
    }
}

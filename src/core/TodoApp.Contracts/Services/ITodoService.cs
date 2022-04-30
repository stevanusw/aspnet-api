using Tasks = System.Threading.Tasks;
using TodoApp.Entities;
using TodoApp.Models.Dtos;

namespace TodoApp.Contracts.Services
{
    public interface ITodoService
    {
        Task<IEnumerable<TodoDto>> GetTodosAsync();
        Task<TodoDto> GetTodoAsync(int id);
        Task<TodoDto> CreateTodoAsync(TodoForCreationDto requestDto);
        Tasks.Task DeleteTodoAsync(int id);
        Tasks.Task UpdateTodoAsync(int id, TodoForUpdateDto requestDto);
        Task<(TodoForUpdateDto DtoToPatch, Todo Entity)> GetTodoForPatchAsync(int id);
        Tasks.Task UpdateTodoFromPatchAsync(TodoForUpdateDto requestDto, Todo entity);
    }
}

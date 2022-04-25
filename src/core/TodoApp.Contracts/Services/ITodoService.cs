using TodoApp.Models.Dtos;

namespace TodoApp.Contracts.Services
{
    public interface ITodoService
    {
        Task<IEnumerable<TodoDto>> GetTodosAsync();
        Task<TodoDto> GetTodoAsync(int id);
        Task<TodoDto> CreateTodoAsync(TodoForCreationDto todo);
        Task DeleteTodoAsync(int id);
        Task UpdateTodoAsync(int id, TodoForUpdateDto todo);
    }
}

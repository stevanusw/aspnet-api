using TodoApp.Models;

namespace TodoApp.Contracts.Services
{
    public interface ITodoService
    {
        Task<IEnumerable<TodoDto>> GetTodosAsync();
        Task<TodoDto> GetTodoAsync(int todoId);
        Task<TodoDto> CreateTodoAsync(TodoForCreationDto todo);
    }
}

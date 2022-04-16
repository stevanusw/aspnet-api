using TodoApp.Models;

namespace TodoApp.Contracts.Services
{
    public interface ITodoService
    {
        Task<IEnumerable<TodoDto>> GetTodosAsync();
    }
}

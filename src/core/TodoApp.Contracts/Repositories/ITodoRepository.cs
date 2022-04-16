using TodoApp.Domain.Entities;

namespace TodoApp.Contracts.Repositories
{
    public interface ITodoRepository
    {
        Task<IEnumerable<Todo>> GetTodosAsync(bool trackChanges);
    }
}

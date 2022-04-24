using TodoApp.Entities;

namespace TodoApp.Contracts.Repositories
{
    public interface ITodoRepository
    {
        Task<IEnumerable<Todo>> GetTodosAsync(bool trackChanges);
        Task<Todo?> GetTodoAsync(int id,  bool trackChanges);
        void CreateTodo(Todo todo);
        void DeleteTodo(Todo todo);
    }
}

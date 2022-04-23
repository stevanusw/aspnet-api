using TodoApp.Entities;

namespace TodoApp.Contracts.Repositories
{
    public interface ITodoRepository
    {
        Task<IEnumerable<Todo>> GetTodosAsync(bool trackChanges);
        Task<Todo?> GetTodoAsync(int todoId,  bool trackChanges);
        void CreateTodo(Todo todo);
    }
}

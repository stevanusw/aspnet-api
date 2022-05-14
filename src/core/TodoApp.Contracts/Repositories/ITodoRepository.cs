using TodoApp.Entities;
using TodoApp.Models.Paging;
using TodoApp.Models.Parameters;

namespace TodoApp.Contracts.Repositories
{
    public interface ITodoRepository
    {
        Task<PagedList<Todo>> GetTodosAsync(TodoParameters parameters, bool trackChanges);
        Task<Todo?> GetTodoAsync(int id,  bool trackChanges);
        void CreateTodo(Todo todo);
        void DeleteTodo(Todo todo);
    }
}

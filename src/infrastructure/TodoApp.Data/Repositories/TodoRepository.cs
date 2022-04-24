using Microsoft.EntityFrameworkCore;
using TodoApp.Contracts.Repositories;
using TodoApp.Entities;

namespace TodoApp.Data.Repositories
{
    internal class TodoRepository : RepositoryBase<Todo>, ITodoRepository
    {
        public TodoRepository(TodoDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Todo>> GetTodosAsync(bool trackChanges)
            => await FindAll(trackChanges)
                .ToListAsync();

        public async Task<Todo?> GetTodoAsync(int id, bool trackChanges)
            => await FindWhere(t => t.Id == id, trackChanges)
                .SingleOrDefaultAsync();

        public void CreateTodo(Todo todo) => Create(todo);
        public void DeleteTodo(Todo todo) => Delete(todo);
    }
}

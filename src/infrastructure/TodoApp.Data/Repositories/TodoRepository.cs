using Microsoft.EntityFrameworkCore;
using TodoApp.Contracts.Repositories;
using TodoApp.Domain.Entities;

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

        public async Task<Todo?> GetTodoAsync(int todoId, bool trackChanges)
            => await FindWhere(t => t.Id == todoId, trackChanges)
            .SingleOrDefaultAsync();

        public void CreateTodo(Todo todo) => Create(todo);
    }
}

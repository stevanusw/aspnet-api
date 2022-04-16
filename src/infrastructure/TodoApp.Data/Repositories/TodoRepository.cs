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
    }
}

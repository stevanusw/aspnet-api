using TodoApp.Contracts.Repositories;
using TodoApp.Domain.Entities;

namespace TodoApp.Data.Repositories
{
    internal class TodoRepository : RepositoryBase<Todo>, ITodoRepository
    {
        public TodoRepository(TodoDbContext dbContext) : base(dbContext)
        {
        }
    }
}

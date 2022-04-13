using TodoApp.Application.Contracts;
using TodoApp.Domain.Entities;

namespace TodoApp.Data.Repositories
{
    public class TodoRepository : RepositoryBase<Todo>, ITodoRepository
    {
        public TodoRepository(TodoDbContext dbContext) : base(dbContext)
        {
        }
    }
}

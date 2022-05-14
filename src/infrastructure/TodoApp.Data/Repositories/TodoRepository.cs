using Microsoft.EntityFrameworkCore;
using TodoApp.Contracts.Repositories;
using TodoApp.Entities;
using TodoApp.Models.Paging;
using TodoApp.Models.Parameters;

namespace TodoApp.Data.Repositories
{
    internal class TodoRepository : RepositoryBase<Todo>, ITodoRepository
    {
        public TodoRepository(TodoDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<PagedList<Todo>> GetTodosAsync(TodoParameters parameters, bool trackChanges)
        {
            var todos = await FindAll(trackChanges)
                .Skip((parameters.PageNo - 1) * parameters.PageSize)
                .Take(parameters.PageSize)
                .ToListAsync();

            var count = await FindAll(false)
                .CountAsync();

            return new PagedList<Todo>(todos, parameters.PageNo, parameters.PageSize, count);
        }

        public async Task<Todo?> GetTodoAsync(int id, bool trackChanges)
            => await FindWhere(t => t.Id == id, trackChanges)
                .SingleOrDefaultAsync();

        public void CreateTodo(Todo todo) => Create(todo);
        public void DeleteTodo(Todo todo) => Delete(todo);
    }
}

using Entities = TodoApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using TodoApp.Contracts.Repositories;

namespace TodoApp.Data.Repositories
{
    internal class TaskRepository : RepositoryBase<Domain.Entities.Task>, ITaskRepository
    {
        public TaskRepository(TodoDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Entities.Task>> GetTasksAsync(int todoId,
            bool trackChanges)
            => await FindWhere(t => t.TodoId == todoId,
                false)
                .ToListAsync();
    }
}

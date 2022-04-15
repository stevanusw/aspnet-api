using TodoApp.Contracts.Repositories;

namespace TodoApp.Data.Repositories
{
    internal class TaskRepository : RepositoryBase<Domain.Entities.Task>, ITaskRepository
    {
        public TaskRepository(TodoDbContext dbContext) : base(dbContext)
        {
        }
    }
}

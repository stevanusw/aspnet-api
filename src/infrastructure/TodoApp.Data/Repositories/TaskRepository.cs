using TodoApp.Application.Contracts;

namespace TodoApp.Data.Repositories
{
    public class TaskRepository : RepositoryBase<Domain.Entities.Task>, ITaskRepository
    {
        public TaskRepository(TodoDbContext dbContext) : base(dbContext)
        {
        }
    }
}

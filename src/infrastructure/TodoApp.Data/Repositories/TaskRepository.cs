using Microsoft.EntityFrameworkCore;
using TodoApp.Contracts.Repositories;

namespace TodoApp.Data.Repositories
{
    internal class TaskRepository : RepositoryBase<Entities.Task>, ITaskRepository
    {
        public TaskRepository(TodoDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Entities.Task>> GetTasksAsync(int todoId, bool trackChanges)
            => await FindWhere(t => t.TodoId == todoId, false)
                .ToListAsync();

        public async Task<Entities.Task?> GetTaskAsync(int todoId, int taskId, bool trackChanges)
            => await FindWhere(t => t.TodoId == todoId && t.Id == taskId, false)
                .SingleOrDefaultAsync();

        public void CreateTask(int todoId, Entities.Task task)
        {
            task.TodoId = todoId;
            Create(task);
        }

        public void DeleteTask(Entities.Task task) => Delete(task);
    }
}

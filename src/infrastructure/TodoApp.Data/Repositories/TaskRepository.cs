using Microsoft.EntityFrameworkCore;
using TodoApp.Contracts.Repositories;
using TodoApp.Data.Extensions;
using TodoApp.Models.Paging;
using TodoApp.Models.Parameters;

namespace TodoApp.Data.Repositories
{
    internal class TaskRepository : RepositoryBase<Entities.Task>, ITaskRepository
    {
        public TaskRepository(TodoDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<PagedList<Entities.Task>> GetTasksAsync(int todoId, TaskParameters parameters, bool trackChanges)
        {
            var tasks = await FindWhere(t => t.TodoId == todoId, trackChanges)
                .Search(parameters.Query)
                .Skip((parameters.PageNo - 1) * parameters.PageSize)
                .Take(parameters.PageSize)
                .ToListAsync();

            var count = await FindWhere(t => t.TodoId == todoId, false)
                .CountAsync();

            return new PagedList<Entities.Task>(tasks, parameters.PageNo, parameters.PageSize, count);
        }

        public async Task<Entities.Task?> GetTaskAsync(int todoId, int taskId, bool trackChanges)
            => await FindWhere(t => t.TodoId == todoId && t.Id == taskId, trackChanges)
                .SingleOrDefaultAsync();

        public void CreateTask(int todoId, Entities.Task task)
        {
            task.TodoId = todoId;

            Create(task);
        }

        public void DeleteTask(Entities.Task task) => Delete(task);
    }
}

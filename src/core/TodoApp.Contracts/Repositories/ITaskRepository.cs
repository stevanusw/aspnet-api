using TodoApp.Models.Paging;
using TodoApp.Models.Parameters;

namespace TodoApp.Contracts.Repositories
{
    public interface ITaskRepository
    {
        Task<PagedList<Entities.Task>> GetTasksAsync(int todoId, TaskParameters parameters, bool trackChanges);
        Task<Entities.Task?> GetTaskAsync(int todoId, int taskId, bool trackChanges);
        void CreateTask(int todoId, Entities.Task task);
        void DeleteTask(Entities.Task task);
    }
}

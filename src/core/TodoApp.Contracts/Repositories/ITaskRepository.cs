namespace TodoApp.Contracts.Repositories
{
    public interface ITaskRepository
    {
        Task<IEnumerable<Entities.Task>> GetTasksAsync(int todoId, bool trackChanges);
        Task<Entities.Task?> GetTaskAsync(int todoId, int taskId, bool trackChanges);
    }
}

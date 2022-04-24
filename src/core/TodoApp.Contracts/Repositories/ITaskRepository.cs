namespace TodoApp.Contracts.Repositories
{
    public interface ITaskRepository
    {
        Task<IEnumerable<Entities.Task>> GetTasksAsync(int todoId, bool trackChanges);
        Task<Entities.Task?> GetTaskAsync(int todoId, int taskId, bool trackChanges);
        void CreateTask(int todoId, Entities.Task task);
        void DeleteTask(Entities.Task task);
    }
}

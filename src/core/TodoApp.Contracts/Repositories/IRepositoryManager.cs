namespace TodoApp.Contracts.Repositories
{
    public interface IRepositoryManager
    {
        ITodoRepository Todo { get; }
        ITaskRepository Task { get; }
        IUserRepository User { get; }
        Task SaveAsync();
    }
}

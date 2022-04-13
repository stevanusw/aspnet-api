namespace TodoApp.Application.Contracts
{
    public interface IRepositoryManager
    {
        ITodoRepository Todo { get; }
        ITaskRepository Task { get; }
        void Save();
    }
}

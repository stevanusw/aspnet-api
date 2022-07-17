namespace TodoApp.Contracts.Services
{
    public interface IServiceManager
    {
        ITodoService Todo { get; }
        ITaskService Task { get; }
        IAuthenticationService Authentication { get; }
    }
}

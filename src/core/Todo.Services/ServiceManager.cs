using TodoApp.Contracts.Repositories;
using TodoApp.Contracts.Services;

namespace Todo.Services
{
    internal class ServiceManager : IServiceManager
    {
        private readonly Lazy<ITodoService> _todoService;
        private readonly Lazy<ITaskService> _taskService;

        public ServiceManager(IRepositoryManager repository)
        {
            _todoService = new Lazy<ITodoService>(() => new TodoService(repository));
            _taskService = new Lazy<ITaskService>(() => new TaskService(repository));
        }

        public ITodoService Todo => _todoService.Value;
        public ITaskService Task => _taskService.Value;
    }
}

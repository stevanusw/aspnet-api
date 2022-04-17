using AutoMapper;
using Microsoft.Extensions.Logging;
using TodoApp.Contracts.Repositories;
using TodoApp.Contracts.Services;

namespace Todo.Services
{
    internal class ServiceManager : IServiceManager
    {
        private readonly Lazy<ITodoService> _todoService;
        private readonly Lazy<ITaskService> _taskService;

        public ServiceManager(IRepositoryManager repository,
            IMapper mapper,
            IServiceProvider provider)
        {
            _todoService = new Lazy<ITodoService>(() => new TodoService(repository, 
                mapper,
                (ILogger<TodoService>)provider.GetService(typeof(ILogger<TodoService>))!));

            _taskService = new Lazy<ITaskService>(() => new TaskService(repository, 
                mapper,
                (ILogger<TaskService>)provider.GetService(typeof(ILogger<TaskService>))!));
        }

        public ITodoService Todo => _todoService.Value;
        public ITaskService Task => _taskService.Value;
    }
}

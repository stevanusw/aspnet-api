using AutoMapper;
using Microsoft.Extensions.Logging;
using TodoApp.Contracts.Repositories;
using TodoApp.Contracts.Services;
using TodoApp.Models.Dtos;

namespace TodoApp.Services
{
    internal class ServiceManager : IServiceManager
    {
        private readonly Lazy<ITodoService> _todoService;
        private readonly Lazy<ITaskService> _taskService;

        public ServiceManager(IRepositoryManager repository, IMapper mapper, IServiceProvider provider)
        {
            _todoService = new Lazy<ITodoService>(() => new TodoService(repository, 
                mapper,
                (ILogger<TodoService>)provider.GetService(typeof(ILogger<TodoService>))!,
                (IDataShaper<TodoDto>)provider.GetService(typeof(IDataShaper<TodoDto>))!,
                (ILinksGenerator<TodoDto>)provider.GetService(typeof(ILinksGenerator<TodoDto>))!));

            _taskService = new Lazy<ITaskService>(() => new TaskService(repository, 
                mapper,
                (ILogger<TaskService>)provider.GetService(typeof(ILogger<TaskService>))!,
                (IDataShaper<TaskDto>)provider.GetService(typeof(IDataShaper<TaskDto>))!));
        }

        public ITodoService Todo => _todoService.Value;
        public ITaskService Task => _taskService.Value;
    }
}

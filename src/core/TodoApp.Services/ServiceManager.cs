using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using TodoApp.Contracts.Repositories;
using TodoApp.Contracts.Services;
using TodoApp.Entities;
using TodoApp.Models.Dtos;

namespace TodoApp.Services
{
    internal class ServiceManager : IServiceManager
    {
        private readonly Lazy<ITodoService> _todoService;
        private readonly Lazy<ITaskService> _taskService;
        private readonly Lazy<IAuthenticationService> _authenticationService;

        public ServiceManager(IRepositoryManager repository, 
            IMapper mapper, 
            IServiceProvider provider,
            IConfiguration configuration,
            UserManager<User> userManager)
        {
            _todoService = new Lazy<ITodoService>(() => new TodoService(repository, 
                mapper,
                (ILogger<TodoService>)provider.GetService(typeof(ILogger<TodoService>))!,
                (IDataShaper<TodoDto>)provider.GetService(typeof(IDataShaper<TodoDto>))!,
                (ILinksGenerator<TodoDto>)provider.GetService(typeof(ILinksGenerator<TodoDto>))!));

            _taskService = new Lazy<ITaskService>(() => new TaskService(repository, 
                mapper,
                (ILogger<TaskService>)provider.GetService(typeof(ILogger<TaskService>))!,
                (IDataShaper<TaskDto>)provider.GetService(typeof(IDataShaper<TaskDto>))!,
                (ILinksGenerator<TaskDto>)provider.GetService(typeof(ILinksGenerator<TaskDto>))!));

            var userService = new UserService(repository);

            _authenticationService = new Lazy<IAuthenticationService>(() =>
                new AuthenticationService((ILogger<AuthenticationService>)provider.GetService(typeof(ILogger<AuthenticationService>))!,
                mapper,
                userManager,
                configuration,
                userService));
        }

        public ITodoService Todo => _todoService.Value;
        public ITaskService Task => _taskService.Value;
        public IAuthenticationService Authentication => _authenticationService.Value;
    }
}

using AutoMapper;
using Microsoft.Extensions.Logging;
using TodoApp.Contracts.Repositories;
using TodoApp.Contracts.Services;
using TodoApp.Models.Dtos;
using TodoApp.Models.Exceptions;

namespace TodoApp.Services
{
    internal class TaskService : ITaskService
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public TaskService(IRepositoryManager repository, IMapper mapper, ILogger logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<TaskDto> GetTaskAsync(int todoId, int taskId)
        {
            var task = await _repository.Task.GetTaskAsync(todoId, taskId, false);
            if (task == null)
            {
                throw new TaskNotFoundException(taskId);
            }

            var dto = _mapper.Map<TaskDto>(task);
            
            return dto;
        }

        public async Task<IEnumerable<TaskDto>> GetTasksAsync(int todoId)
        {
            _logger.LogDebug(@"Get tasks with Todo Id: {todoId}.", todoId);

            var tasks = await _repository.Task.GetTasksAsync(todoId,
                false);
            var dto = _mapper.Map<IEnumerable<TaskDto>>(tasks);

            return dto;
        }

        public async Task<TaskDto> CreateTaskAsync(int todoId, TaskForCreationDto task)
        {
            var todo = await _repository.Todo.GetTodoAsync(todoId, false);
            if (todo == null)
            {
                throw new TodoNotFoundException(todoId);
            }

            var entity = _mapper.Map<Entities.Task>(task);
            _repository.Task.CreateTask(todoId, entity);
            await _repository.SaveAsync();

            var dto = _mapper.Map<TaskDto>(entity);

            return dto;
        }
    }
}

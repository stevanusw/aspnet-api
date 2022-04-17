using AutoMapper;
using Microsoft.Extensions.Logging;
using TodoApp.Contracts.Repositories;
using TodoApp.Contracts.Services;
using TodoApp.Models;

namespace Todo.Services
{
    internal class TaskService : ITaskService
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public TaskService(IRepositoryManager repository,
            IMapper mapper,
            ILogger logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<TaskDto>> GetTasksAsync(int todoId)
        {
            _logger.LogDebug(@"Get tasks with Todo Id: {todoId}.", todoId);

            var tasks = await _repository.Task.GetTasksAsync(todoId,
                false);
            var dto = _mapper.Map<IEnumerable<TaskDto>>(tasks);

            return dto;
        }
    }
}

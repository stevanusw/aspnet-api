using AutoMapper;
using TodoApp.Contracts.Repositories;
using TodoApp.Contracts.Services;
using TodoApp.Models;

namespace Todo.Services
{
    internal class TaskService : ITaskService
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;

        public TaskService(IRepositoryManager repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TaskDto>> GetTasksAsync(int todoId)
        {
            var tasks = await _repository.Task.GetTasksAsync(todoId,
                false);
            var dto = _mapper.Map<IEnumerable<TaskDto>>(tasks);

            return dto;
        }
    }
}

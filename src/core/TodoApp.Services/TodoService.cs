using AutoMapper;
using Microsoft.Extensions.Logging;
using TodoApp.Contracts.Repositories;
using TodoApp.Contracts.Services;
using TodoApp.Models;

namespace Todo.Services
{
    internal class TodoService : ITodoService
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public TodoService(IRepositoryManager repository,
            IMapper mapper,
            ILogger logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<TodoDto>> GetTodosAsync()
        {
            _logger.LogInformation("Get Todos.");

            var todos = await _repository.Todo.GetTodosAsync(false);
            var dto = _mapper.Map<IEnumerable<TodoDto>>(todos);

            return dto;
        }
    }
}

using AutoMapper;
using TodoApp.Contracts.Repositories;
using TodoApp.Contracts.Services;
using TodoApp.Models;

namespace Todo.Services
{
    internal class TodoService : ITodoService
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;

        public TodoService(IRepositoryManager repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TodoDto>> GetTodosAsync()
        {
            var todos = await _repository.Todo.GetTodosAsync(false);
            var dto = _mapper.Map<IEnumerable<TodoDto>>(todos);

            return dto;
        }
    }
}

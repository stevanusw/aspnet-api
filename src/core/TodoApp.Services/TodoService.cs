using AutoMapper;
using Microsoft.Extensions.Logging;
using Tasks = System.Threading.Tasks;
using TodoApp.Contracts.Repositories;
using TodoApp.Contracts.Services;
using TodoApp.Entities;
using TodoApp.Models.Dtos;
using TodoApp.Models.Exceptions;

namespace TodoApp.Services
{
    internal class TodoService : ITodoService
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public TodoService(IRepositoryManager repository, IMapper mapper, ILogger logger)
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

        public async Task<TodoDto> GetTodoAsync(int id)
        {
            var todo = await _repository.Todo.GetTodoAsync(id, false);
            if (todo == null)
            {
                throw new TodoNotFoundException(id);
            }

            var dto = _mapper.Map<TodoDto>(todo);

            return dto;
        }

        public async Task<TodoDto> CreateTodoAsync(TodoForCreationDto todo)
        {
            var entity = _mapper.Map<Todo>(todo);

            _repository.Todo.CreateTodo(entity);
            await _repository.SaveAsync();

            var dto = _mapper.Map<TodoDto>(entity);

            return dto;
        }

        public async Tasks.Task DeleteTodoAsync(int id)
        {
            var todo = await _repository.Todo.GetTodoAsync(id, false);
            if (todo == null)
            {
                throw new TodoNotFoundException(id);
            }

            _repository.Todo.DeleteTodo(todo);
            await _repository.SaveAsync();
        }

        public async Tasks.Task UpdateTodoAsync(int id, TodoForUpdateDto todo)
        {
            var entity = await _repository.Todo.GetTodoAsync(id, true);
            if (entity == null)
            {
                throw new TodoNotFoundException(id);
            }

            _mapper.Map(todo, entity);
            await _repository.SaveAsync();
        }
    }
}

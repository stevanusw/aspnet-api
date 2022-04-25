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

            var entities = await _repository.Todo.GetTodosAsync(false);
            var response = _mapper.Map<IEnumerable<TodoDto>>(entities);

            return response;
        }

        public async Task<TodoDto> GetTodoAsync(int id)
        {
            var entity = await _repository.Todo.GetTodoAsync(id, false);
            if (entity == null)
            {
                throw new TodoNotFoundException(id);
            }

            var response = _mapper.Map<TodoDto>(entity);

            return response;
        }

        public async Task<TodoDto> CreateTodoAsync(TodoForCreationDto request)
        {
            var entity = _mapper.Map<Todo>(request);

            _repository.Todo.CreateTodo(entity);
            await _repository.SaveAsync();

            var response = _mapper.Map<TodoDto>(entity);

            return response;
        }

        public async Tasks.Task DeleteTodoAsync(int id)
        {
            var entity = await _repository.Todo.GetTodoAsync(id, false);
            if (entity == null)
            {
                throw new TodoNotFoundException(id);
            }

            _repository.Todo.DeleteTodo(entity);
            await _repository.SaveAsync();
        }

        public async Tasks.Task UpdateTodoAsync(int id, TodoForUpdateDto request)
        {
            var entity = await _repository.Todo.GetTodoAsync(id, true);
            if (entity == null)
            {
                throw new TodoNotFoundException(id);
            }

            _mapper.Map(request, entity);
            await _repository.SaveAsync();
        }

        public async Task<(TodoForUpdateDto DtoToPatch, Todo Entity)> GetTodoForPatchAsync(int id)
        {
            var entity = await _repository.Todo.GetTodoAsync(id, true);
            if (entity == null)
            {
                throw new TodoNotFoundException(id);
            }

            var dtoToPatch = _mapper.Map<TodoForUpdateDto>(entity);

            return (dtoToPatch, entity);
        }

        public async Tasks.Task UpdateTodoFromPatchAsync(TodoForUpdateDto request, Todo entity)
        {
            _mapper.Map(request, entity);

            await _repository.SaveAsync();
        }
    }
}

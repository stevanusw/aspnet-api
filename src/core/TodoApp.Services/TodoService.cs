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
            var responseDto = _mapper.Map<IEnumerable<TodoDto>>(entities);

            return responseDto;
        }

        public async Task<TodoDto> GetTodoAsync(int id)
        {
            var entity = await GetTodoAndCheckIfItExists(id, false);
            var responseDto = _mapper.Map<TodoDto>(entity);

            return responseDto;
        }

        public async Task<TodoDto> CreateTodoAsync(TodoForCreationDto requestDto)
        {
            var entity = _mapper.Map<Todo>(requestDto);

            _repository.Todo.CreateTodo(entity);
            
            await _repository.SaveAsync();

            var responseDto = _mapper.Map<TodoDto>(entity);

            return responseDto;
        }

        public async Tasks.Task DeleteTodoAsync(int id)
        {
            var entity = await GetTodoAndCheckIfItExists(id, false);

            _repository.Todo.DeleteTodo(entity);

            await _repository.SaveAsync();
        }

        public async Tasks.Task UpdateTodoAsync(int id, TodoForUpdateDto requestDto)
        {
            var entity = await GetTodoAndCheckIfItExists(id, true);

            _mapper.Map(requestDto, entity);

            await _repository.SaveAsync();
        }

        public async Task<(TodoForUpdateDto DtoToPatch, Todo Entity)> GetTodoForPatchAsync(int id)
        {
            var entity = await GetTodoAndCheckIfItExists(id, true);
            var dtoToPatch = _mapper.Map<TodoForUpdateDto>(entity);

            return (dtoToPatch, entity);
        }

        public async Tasks.Task UpdateTodoFromPatchAsync(TodoForUpdateDto requestDto, Todo entity)
        {
            _mapper.Map(requestDto, entity);

            await _repository.SaveAsync();
        }

        private async Task<Todo> GetTodoAndCheckIfItExists(int id, bool trackChanges)
        {
            var entity = await _repository.Todo.GetTodoAsync(id, trackChanges);
            if (entity == null)
            {
                throw new TodoNotFoundException(id);
            }

            return entity;
        }
    }
}

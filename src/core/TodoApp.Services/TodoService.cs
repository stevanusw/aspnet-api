using AutoMapper;
using Microsoft.Extensions.Logging;
using System.Dynamic;
using TodoApp.Contracts.Repositories;
using TodoApp.Contracts.Services;
using TodoApp.Entities;
using TodoApp.Models.Dtos;
using TodoApp.Models.Exceptions;
using TodoApp.Models.Links;
using TodoApp.Models.Paging;
using TodoApp.Models.Parameters;
using Tasks = System.Threading.Tasks;

namespace TodoApp.Services
{
    internal class TodoService : ITodoService
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<TodoService> _logger;
        private readonly IDataShaper<TodoDto> _dataShaper;
        private readonly ILinksGenerator<TodoDto> _linksGenerator;

        public TodoService(IRepositoryManager repository, 
            IMapper mapper, 
            ILogger<TodoService> logger, 
            IDataShaper<TodoDto> dataShaper,
            ILinksGenerator<TodoDto> linksGenerator)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _dataShaper = dataShaper;
            _linksGenerator = linksGenerator;
        }

        public async Task<(LinkResponse Dto, PageInfo PageInfo)> GetTodosAsync(LinkParameters linkParameters)
        {
            _logger.LogInformation("Get Todos.");

            var pagedEntities = await _repository.Todo.GetTodosAsync((TodoParameters)linkParameters.RequestParameters, false);
            var dtos = _mapper.Map<IEnumerable<TodoDto>>(pagedEntities);
            var responseDto = _linksGenerator.TryGenerateLinks(dtos, linkParameters.RequestParameters.Fields);

            return (Dto: responseDto, PageInfo: pagedEntities.PageInfo);
        }

        public async Task<ExpandoObject> GetTodoAsync(int id, TodoParameters parameters)
        {
            var entity = await GetTodoAndCheckIfItExists(id, false);
            var responseDto = _mapper.Map<TodoDto>(entity);
            var shapedDto = _dataShaper.Shape(responseDto, parameters.Fields);

            return shapedDto.Dto;
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

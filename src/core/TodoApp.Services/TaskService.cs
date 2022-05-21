using AutoMapper;
using Microsoft.Extensions.Logging;
using System.Dynamic;
using TodoApp.Contracts.Repositories;
using TodoApp.Contracts.Services;
using TodoApp.Models.Dtos;
using TodoApp.Models.Exceptions;
using TodoApp.Models.Paging;
using TodoApp.Models.Parameters;

namespace TodoApp.Services
{
    internal class TaskService : ITaskService
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<TaskService> _logger;
        private readonly IDataShaper<TaskDto> _dataShaper;

        public TaskService(IRepositoryManager repository, IMapper mapper, ILogger<TaskService> logger, IDataShaper<TaskDto> dataShaper)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _dataShaper = dataShaper;
        }

        public async Task<(IEnumerable<ExpandoObject> Dto, PageInfo PageInfo)> GetTasksAsync(int todoId, TaskParameters parameters)
        {
            _logger.LogDebug(@"Get tasks with Todo Id: {todoId}.", todoId);

            var pagedEntities = await _repository.Task.GetTasksAsync(todoId, parameters, false);
            var responseDto = _mapper.Map<IEnumerable<TaskDto>>(pagedEntities);
            var shapedDto = _dataShaper.Shape(responseDto, parameters.Fields);

            return (Dto: shapedDto, PageInfo: pagedEntities.PageInfo);
        }

        public async Task<ExpandoObject> GetTaskAsync(int todoId, int taskId, TaskParameters parameters)
        {
            var entity = await GetTaskAndCheckIfItExists(todoId, taskId, false);
            var responseDto = _mapper.Map<TaskDto>(entity);
            var shapedDto = _dataShaper.Shape(responseDto, parameters.Fields);

            return shapedDto;
        }

        public async Task<TaskDto> CreateTaskAsync(int todoId, TaskForCreationDto requestDto)
        {
            await CheckIfTodoExists(todoId, false);

            var taskEntity = _mapper.Map<Entities.Task>(requestDto);

            _repository.Task.CreateTask(todoId, taskEntity);

            await _repository.SaveAsync();

            var responseDto = _mapper.Map<TaskDto>(taskEntity);

            return responseDto;
        }

        public async Task DeleteTaskAsync(int todoId, int taskId)
        {
            await CheckIfTodoExists(todoId, false);

            var entity = await GetTaskAndCheckIfItExists(todoId, taskId, false);

            _repository.Task.DeleteTask(entity);

            await _repository.SaveAsync();
        }

        public async Task UpdateTaskAsync(int todoId, int taskId, TaskForUpdateDto requestDto)
        {
            await CheckIfTodoExists(todoId, false);

            var entity = await GetTaskAndCheckIfItExists(todoId, taskId, true);

            _mapper.Map(requestDto, entity);

            await _repository.SaveAsync();
        }

        private async Task CheckIfTodoExists(int id, bool trackChanges)
        {
            var entity = await _repository.Todo.GetTodoAsync(id, trackChanges);
            if (entity == null)
            {
                throw new TodoNotFoundException(id);
            }
        }

        private async Task<Entities.Task> GetTaskAndCheckIfItExists(int todoId, int taskId, bool trackChanges)
        {
            var entity = await _repository.Task.GetTaskAsync(todoId, taskId, true);
            if (entity == null)
            {
                throw new TaskNotFoundException(taskId);
            }

            return entity;
        }
    }
}

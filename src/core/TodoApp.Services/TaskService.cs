using AutoMapper;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger _logger;

        public TaskService(IRepositoryManager repository, IMapper mapper, ILogger logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<TaskDto> GetTaskAsync(int todoId, int taskId)
        {
            var entity = await GetTaskAndCheckIfItExists(todoId, taskId, false);
            var responseDto = _mapper.Map<TaskDto>(entity);
            
            return responseDto;
        }

        public async Task<(IEnumerable<TaskDto> Dto, PageInfo PageInfo)> GetTasksAsync(int todoId, TaskParameters parameters)
        {
            _logger.LogDebug(@"Get tasks with Todo Id: {todoId}.", todoId);

            var pagedEntities = await _repository.Task.GetTasksAsync(todoId, parameters, false);
            var responseDto = _mapper.Map<IEnumerable<TaskDto>>(pagedEntities);

            return (Dto: responseDto, PageInfo: pagedEntities.PageInfo);
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

﻿using AutoMapper;
using Microsoft.Extensions.Logging;
using TodoApp.Contracts.Repositories;
using TodoApp.Contracts.Services;
using TodoApp.Models.Dtos;
using TodoApp.Models.Exceptions;

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
            var entity = await _repository.Task.GetTaskAsync(todoId, taskId, false);
            if (entity == null)
            {
                throw new TaskNotFoundException(taskId);
            }

            var response = _mapper.Map<TaskDto>(entity);
            
            return response;
        }

        public async Task<IEnumerable<TaskDto>> GetTasksAsync(int todoId)
        {
            _logger.LogDebug(@"Get tasks with Todo Id: {todoId}.", todoId);

            var entities = await _repository.Task.GetTasksAsync(todoId, false);

            var response = _mapper.Map<IEnumerable<TaskDto>>(entities);

            return response;
        }

        public async Task<TaskDto> CreateTaskAsync(int todoId, TaskForCreationDto request)
        {
            var todoEntity = await _repository.Todo.GetTodoAsync(todoId, false);
            if (todoEntity == null)
            {
                throw new TodoNotFoundException(todoId);
            }

            var taskEntity = _mapper.Map<Entities.Task>(request);
            _repository.Task.CreateTask(todoId, taskEntity);
            await _repository.SaveAsync();

            var response = _mapper.Map<TaskDto>(taskEntity);

            return response;
        }

        public async Task DeleteTaskAsync(int todoId, int taskId)
        {
            var todoEntity = await _repository.Todo.GetTodoAsync(todoId, false);
            if (todoEntity == null)
            {
                throw new TodoNotFoundException(todoId);
            }

            var taskEntity = await _repository.Task.GetTaskAsync(todoId, taskId, false);
            if (taskEntity == null)
            {
                throw new TaskNotFoundException(taskId);
            }

            _repository.Task.DeleteTask(taskEntity);
            await _repository.SaveAsync();
        }

        public async Task UpdateTaskAsync(int todoId, int taskId, TaskForUpdateDto request)
        {
            var todoEntity = await _repository.Todo.GetTodoAsync(todoId, false);
            if (todoEntity == null)
            {
                throw new TodoNotFoundException(todoId);
            }

            var taskEntity = await _repository.Task.GetTaskAsync(todoId, taskId, true);
            if (taskEntity == null)
            {
                throw new TaskNotFoundException(taskId);
            }

            _mapper.Map(request, taskEntity);
            await _repository.SaveAsync();
        }
    }
}

﻿using AutoMapper;
using Microsoft.Extensions.Logging;
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
    }
}

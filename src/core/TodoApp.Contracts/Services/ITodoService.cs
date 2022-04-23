﻿using TodoApp.Models.Dtos;

namespace TodoApp.Contracts.Services
{
    public interface ITodoService
    {
        Task<IEnumerable<TodoDto>> GetTodosAsync();
        Task<TodoDto> GetTodoAsync(int id);
        Task<TodoDto> CreateTodoAsync(TodoForCreationDto todo);
    }
}

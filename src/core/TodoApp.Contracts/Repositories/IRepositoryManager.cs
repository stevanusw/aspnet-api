﻿namespace TodoApp.Contracts.Repositories
{
    public interface IRepositoryManager
    {
        ITodoRepository Todo { get; }
        ITaskRepository Task { get; }
        Task SaveAsync();
    }
}
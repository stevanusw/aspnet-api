﻿using TodoApp.Application.Contracts;

namespace TodoApp.Data.Repositories
{
    public sealed class RepositoryManager : IRepositoryManager
    {
        private readonly TodoDbContext _dbContext;
        private readonly Lazy<ITodoRepository> _todoRepository;
        private readonly Lazy<ITaskRepository> _taskRepository;

        public RepositoryManager(TodoDbContext dbContext)
        {
            _dbContext = dbContext;
            _todoRepository = new Lazy<ITodoRepository>(() => new TodoRepository(_dbContext));
            _taskRepository = new Lazy<ITaskRepository>(() => new TaskRepository(_dbContext));
        }

        public ITodoRepository Todo => _todoRepository.Value;
        public ITaskRepository Task => _taskRepository.Value;

        public void Save() => _dbContext.SaveChanges();
    }
}
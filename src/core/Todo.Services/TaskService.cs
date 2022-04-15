using TodoApp.Contracts.Repositories;
using TodoApp.Contracts.Services;

namespace Todo.Services
{
    internal class TaskService : ITaskService
    {
        private readonly IRepositoryManager _repository;

        public TaskService(IRepositoryManager repository)
        {
            _repository = repository;
        }
    }
}

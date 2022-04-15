using TodoApp.Contracts.Repositories;
using TodoApp.Contracts.Services;

namespace Todo.Services
{
    internal class TodoService : ITodoService
    {
        private readonly IRepositoryManager _repository;

        public TodoService(IRepositoryManager repository)
        {
            _repository = repository;
        }
    }
}

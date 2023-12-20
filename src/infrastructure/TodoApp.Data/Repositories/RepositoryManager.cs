using TodoApp.Contracts.Repositories;

namespace TodoApp.Data.Repositories
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly TodoDbContext _dbContext;
        private readonly Lazy<ITodoRepository> _todoRepository;
        private readonly Lazy<ITaskRepository> _taskRepository;
        private readonly Lazy<IUserRepository> _userRepository;

        public RepositoryManager(TodoDbContext dbContext)
        {
            _dbContext = dbContext;
            _todoRepository = new Lazy<ITodoRepository>(() => new TodoRepository(_dbContext));
            _taskRepository = new Lazy<ITaskRepository>(() => new TaskRepository(_dbContext));
            _userRepository = new Lazy<IUserRepository>(() => new UserRepository(_dbContext));
        }

        public ITodoRepository Todo => _todoRepository.Value;
        public ITaskRepository Task => _taskRepository.Value;
        public IUserRepository User => _userRepository.Value;

        public async Task SaveAsync() => await _dbContext.SaveChangesAsync();
    }
}

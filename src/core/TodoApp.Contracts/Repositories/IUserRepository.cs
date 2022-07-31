using TodoApp.Entities;

namespace TodoApp.Contracts.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetUserByRefreshTokenAsync(string refreshToken, bool trackChanges);
    }
}

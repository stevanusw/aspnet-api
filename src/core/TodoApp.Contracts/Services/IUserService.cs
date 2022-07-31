using TodoApp.Entities;

namespace TodoApp.Contracts.Services
{
    public interface IUserService
    {
        Task<User> GetUserByRefreshTokenAsync(string refreshToken);
    }
}

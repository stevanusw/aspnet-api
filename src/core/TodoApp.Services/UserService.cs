using TodoApp.Contracts.Repositories;
using TodoApp.Contracts.Services;
using TodoApp.Entities;
using TodoApp.Models.Exceptions;

namespace TodoApp.Services
{
    internal class UserService : IUserService
    {
        private readonly IRepositoryManager _repository;

        public UserService(IRepositoryManager repository)
        {
            _repository = repository;
        }

        public async Task<User> GetUserByRefreshTokenAsync(string refreshToken, bool trackChanges)
        {
            var user = await _repository.User.GetUserByRefreshTokenAsync(refreshToken, trackChanges);
            if (user == null)
            {
                throw new RefreshTokenNotFoundException();
            }

            return user;
        }
    }
}

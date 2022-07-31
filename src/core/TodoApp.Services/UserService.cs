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

        public async Task<User> GetUserByRefreshTokenAsync(string refreshToken)
        {
            var user = await _repository.User.GetUserByRefreshTokenAsync(refreshToken, false);
            if (user == null)
            {
                throw new UserNotFoundException();
            }

            return user;
        }
    }
}

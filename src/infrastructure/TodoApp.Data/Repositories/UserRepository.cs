using Microsoft.EntityFrameworkCore;
using TodoApp.Contracts.Repositories;
using TodoApp.Entities;

namespace TodoApp.Data.Repositories
{
    internal class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(TodoDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<User?> GetUserByRefreshTokenAsync(string refreshToken, bool trackChanges)
        {
            var user = await FindWhere(u => u.RefreshToken == refreshToken, trackChanges)
                .SingleOrDefaultAsync();

            return user;
        }
    }
}

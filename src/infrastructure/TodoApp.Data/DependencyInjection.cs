using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TodoApp.Contracts.Repositories;
using TodoApp.Data.Repositories;

namespace TodoApp.Data
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureData(this IServiceCollection services,
            IConfiguration configuration)
        {
            return services.AddSqlServer<TodoDbContext>(configuration.GetConnectionString("SqlConnection"))
                .AddScoped<IRepositoryManager, RepositoryManager>();
        }
    }
}

using Microsoft.Extensions.DependencyInjection;
using TodoApp.Application.Contracts;
using TodoApp.Data.Repositories;

namespace TodoApp.Data
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInrastructureData(this IServiceCollection services)
            => services.AddScoped<IRepositoryManager, RepositoryManager>();
    }
}

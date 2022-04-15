using Microsoft.Extensions.DependencyInjection;
using TodoApp.Contracts.Services;

namespace Todo.Services
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services)
            => services.AddScoped<IServiceManager, ServiceManager>();
    }
}

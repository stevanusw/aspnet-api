using Microsoft.Extensions.DependencyInjection;
using TodoApp.Contracts.Services;
using TodoApp.Services;

namespace Todo.Services
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services)
        {
            return services.AddScoped<IServiceManager, ServiceManager>()
                .AddAutoMapper(typeof(MappingProfile));
        }
    }
}

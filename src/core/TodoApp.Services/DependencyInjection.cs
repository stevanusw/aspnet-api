using Microsoft.Extensions.DependencyInjection;
using TodoApp.Contracts.Services;

namespace TodoApp.Services
{
    public static class DependencyInjection
    {
        public static IServiceCollection ConfigureCoreServices(this IServiceCollection services)
        {
            return services.AddScoped<IServiceManager, ServiceManager>()
                .AddAutoMapper(typeof(MappingProfile))
                .AddSingleton(typeof(IDataShaper<>), typeof(DataShaper<>));
        }
    }
}

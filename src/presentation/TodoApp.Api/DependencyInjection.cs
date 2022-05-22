using Microsoft.Extensions.DependencyInjection;
using TodoApp.Api.Filters;
using TodoApp.Api.Utilities;
using TodoApp.Contracts.Services;

namespace TodoApp.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection ConfigureFilters(this IServiceCollection services)
        {
            return services.AddScoped<RequestDtoValidationFilter>();
        }

        public static IServiceCollection ConfigureUtilities(this IServiceCollection services)
        {
            return services.AddSingleton(typeof(ILinksGenerator<>), typeof(LinksGenerator<>));
        }
    }
}

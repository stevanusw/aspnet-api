using Microsoft.Extensions.DependencyInjection;
using TodoApp.Api.Filters;

namespace TodoApp.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection ConfigureFilters(this IServiceCollection services)
        {
            return services.AddScoped<RequestDtoValidationFilter>();
        }
    }
}

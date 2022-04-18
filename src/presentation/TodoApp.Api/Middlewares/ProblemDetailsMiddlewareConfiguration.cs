using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace TodoApp.Api.Middlewares
{
    public static class ProblemDetailsMiddlewareConfiguration
    {
        public static IServiceCollection ConfigureProblemDetails(this IServiceCollection services,
            IHostEnvironment env)
        {
            return services.AddProblemDetails(options =>
            {
                options.IncludeExceptionDetails = (ctx, ex) => env.IsDevelopment();
            });
        }

        public static IApplicationBuilder AddProblemDetails(this IApplicationBuilder builder)
        {
            return builder.UseProblemDetails();
        }
    }
}

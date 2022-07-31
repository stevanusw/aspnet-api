using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TodoApp.Models.Exceptions;

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
                options.MapToStatusCode<NotFoundException>(StatusCodes.Status404NotFound);
                options.MapToStatusCode<BadRequestException>(StatusCodes.Status400BadRequest);
            });
        }

        public static IApplicationBuilder AddProblemDetails(this IApplicationBuilder builder)
        {
            return builder.UseProblemDetails();
        }
    }
}

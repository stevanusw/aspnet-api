using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
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
            return services.AddScoped<RequestDtoValidationFilter>()
                .AddScoped<MediaTypeResolverFilter>();
        }

        public static IServiceCollection ConfigureUtilities(this IServiceCollection services)
        {
            return services.AddSingleton(typeof(ILinksGenerator<>), typeof(LinksGenerator<>));
        }

        public static IServiceCollection ConfigureMediaTypes(this IServiceCollection services)
        {
            return services.Configure<MvcOptions>(options =>
            {
                var systemTextJsonOutputFormatter = options.OutputFormatters
                    .OfType<SystemTextJsonOutputFormatter>()?.FirstOrDefault();

                if (systemTextJsonOutputFormatter != null)
                {
                    systemTextJsonOutputFormatter.SupportedMediaTypes.Add("application/vnd.todoapp.hateoas+json");
                    systemTextJsonOutputFormatter.SupportedMediaTypes.Add("application/vnd.todoapp.apiroot");
                }
            });
        }

        public static IServiceCollection ConfigureApiVersioning(this IServiceCollection services)
        {
            return services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
            });
        }
    }
}

using Microsoft.AspNetCore.Identity;
using TodoApp.Data;
using TodoApp.Entities;

namespace TodoApp.WebApi
{
    internal static class DependencyInjection
    {
        public static IServiceCollection ConfigureCors(this IServiceCollection services)
            => services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder
                    => builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .WithExposedHeaders("X-Pagination"));
            });

        public static IServiceCollection ConfigureIdentity(this IServiceCollection services)
        {
            var builder = services.AddIdentity<User, IdentityRole>(options =>
            {
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 10;

                options.User.RequireUniqueEmail = true;
            })
                .ConfigureStore()
                .AddDefaultTokenProviders();

            return services;
        }
    }
}

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
    }
}

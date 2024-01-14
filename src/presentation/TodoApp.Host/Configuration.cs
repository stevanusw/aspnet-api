using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using TodoApp.Api;
using TodoApp.Api.Filters;
using TodoApp.Api.Utilities;
using TodoApp.Contracts.Repositories;
using TodoApp.Contracts.Services;
using TodoApp.Data;
using TodoApp.Data.Repositories;
using TodoApp.Entities;
using TodoApp.Models.Configuration;
using TodoApp.Models.Exceptions;
using TodoApp.Services;

namespace TodoApp.Host
{
    public static class Configuration
    {
        public static IServiceCollection ConfigureCoreServices(this IServiceCollection services)
        {
            return services.AddScoped<IServiceManager, ServiceManager>()
                .AddAutoMapper(typeof(MappingProfile))
                .AddSingleton(typeof(IDataShaper<>), typeof(DataShaper<>));
        }

        public static IServiceCollection ConfigureInfrastructureData(this IServiceCollection services,
            IConfiguration configuration)
        {
            return services.AddSqlServer<TodoDbContext>(configuration.GetConnectionString("SqlConnection"))
                .AddScoped<IRepositoryManager, RepositoryManager>();
        }

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
                .AddEntityFrameworkStores<TodoDbContext>()
                .AddDefaultTokenProviders();

            return services;
        }

        public static IServiceCollection ConfigureCors(this IServiceCollection services)
           => services.AddCors(options =>
           {
               options.AddPolicy("CorsPolicy", builder
                   => builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader()
                       .WithExposedHeaders("X-Pagination"));
           });

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

        public static IServiceCollection ConfigureJWT(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettings = configuration.GetSection("JwtSettings");

            services.Configure<JwtConfiguration>(jwtSettings)
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,

                        ValidIssuer = jwtSettings["issuer"],
                        ValidAudience = jwtSettings["audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["secret"]!))
                    };
                });

            return services;
        }

        public static IServiceCollection ConfigureSwagger(this IServiceCollection services)
        {
            return services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Todo API",
                    Version = "v1",
                    Description = "Todo API by stevanusw"
                });

                options.SwaggerDoc("v2", new OpenApiInfo
                {
                    Title = "Todo API",
                    Version = "v2",
                    Description = "Todo API by stevanusw"
                });

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Place to add JWT with Bearer",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Name = "Bearer"
                        },
                        new List<string>()
                    }
                });

                var xmlFile = $"{typeof(AssemblyReference).Assembly.GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
            });
        }

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

        public static MvcOptions ConfigureJsonPatchInputFormatter(this MvcOptions options)
        {
            var formatter = new ServiceCollection()
                .AddLogging()
                .AddMvc()
                .AddNewtonsoftJson()
                .Services.BuildServiceProvider()
                .GetRequiredService<IOptions<MvcOptions>>().Value
                .InputFormatters.OfType<NewtonsoftJsonPatchInputFormatter>().First();

            options.InputFormatters.Insert(0, formatter);

            return options;
        }
    }
}

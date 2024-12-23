using Hellang.Middleware.ProblemDetails;
using Newtonsoft.Json.Serialization;
using System.Text.Json.Serialization;
using TodoApp.Api;
using TodoApp.Host;
using TodoApp.Logging;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Host.ConfigureSerilog(builder.Configuration,
    builder.Environment);

builder.Services.ConfigureInfrastructureData(builder.Configuration)
    .ConfigureCoreServices()
    .ConfigureProblemDetails(builder.Environment)
    .ConfigureFilters()
    .ConfigureUtilities()
    //.ConfigureMediaTypes() // Must when ReturnHttpNotAcceptable = true.
    .ConfigureApiVersioning()
    .ConfigureCors()
    .ConfigureIdentity()
    .ConfigureJWT(builder.Configuration)
    .ConfigureSwagger();

builder.Services.AddControllers(options =>
    {
        options.ConfigureJsonPatchInputFormatter();
    })
    .AddApplicationPart(typeof(AssemblyReference).Assembly)
    .ConfigureApiBehaviorOptions(options =>
    {
        options.SuppressModelStateInvalidFilter = true;
    })
    .AddJsonOptions(options =>
    {
        // To show friendly string enum in Swagger.
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    })
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
    }); // To fix contractResolver showing from JsonPatchDocument in Swagger.

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer()
    .AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseProblemDetails();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger()
        .UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "Todo API v1");
            options.SwaggerEndpoint("/swagger/v2/swagger.json", "Todo API v2");
        });
}
else 
{
    app.UseHsts();
}

app.UseHttpsRedirection()
    .UseCors("CorsPolicy")
    .UseAuthentication()
    .UseAuthorization();

app.MapControllers();

app.Run();

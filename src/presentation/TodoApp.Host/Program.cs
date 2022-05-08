using TodoApp.Api;
using TodoApp.Api.Formatters;
using TodoApp.Api.Middlewares;
using TodoApp.Data;
using TodoApp.Logging;
using TodoApp.Services;
using TodoApp.WebApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Host.ConfigureSerilog(builder.Configuration,
    builder.Environment);

builder.Services.ConfigureInfrastructureData(builder.Configuration)
    .ConfigureCoreServices()
    .ConfigureProblemDetails(builder.Environment)
    .ConfigureFilters()
    .ConfigureCors();

builder.Services.AddControllers(options =>
    {
        options.ConfigureJsonPatchInputFormatter();
    })
    .AddApplicationPart(typeof(AssemblyReference).Assembly)
    .ConfigureApiBehaviorOptions(options =>
    {
        options.SuppressModelStateInvalidFilter = true;
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.AddProblemDetails();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Sinks.MSSqlServer;

namespace TodoApp.Logging
{
    public static class Configuration
    {
        public static IHostBuilder ConfigureSerilog(this IHostBuilder builder,
            IConfiguration config,
            IHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                Serilog.Debugging.SelfLog.Enable(message => Console.WriteLine(message));
            }

            return builder
                .ConfigureLogging((context, logging) =>
                {
                    logging.ClearProviders();
                })
                .UseSerilog((context, logging) =>
                {
                    var connectionString = config.GetConnectionString("SqlConnection");
                    var sqlOptions = new MSSqlServerSinkOptions
                    {
                        AutoCreateSqlTable = false,
                        SchemaName = "dbo",
                        TableName = "Logs",
                    };

                    var columnOptions = new ColumnOptions();
                    columnOptions.TimeStamp.ColumnName = "Timestamp"; 
                    columnOptions.TimeStamp.ConvertToUtc = true;

                    logging
                        .ReadFrom.Configuration(config)
                        .WriteTo.Console()
                        .WriteTo.MSSqlServer(connectionString,
                            sqlOptions,
                            columnOptions: columnOptions);
                });
        }
    }
}
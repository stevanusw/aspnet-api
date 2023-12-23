using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Options;

namespace TodoApp.Host
{
    public static class Configuration
    {
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

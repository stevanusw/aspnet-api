using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace TodoApp.Api.Formatters
{
    public static class JsonPatchInputFormatterConfiguration
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

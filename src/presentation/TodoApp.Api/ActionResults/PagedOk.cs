using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Text.Json;
using TodoApp.Models.Paging;

namespace TodoApp.Api.ActionResults
{
    public class PagedOk<T> : IActionResult where T : IEnumerable
    {
        public T Items { get; }
        public PageInfo PageInfo { get; }

        public PagedOk(T items, PageInfo pageInfo)
        {
            Items = items;
            PageInfo = pageInfo;
        }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            var serializeOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };

            context.HttpContext.Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(PageInfo, serializeOptions));

            await new OkObjectResult(Items).ExecuteResultAsync(context);
        }
    }
}

using Microsoft.AspNetCore.Http;
using System.Collections;
using TodoApp.Api.ActionResults;
using TodoApp.Models.Paging;

namespace TodoApp.Api.Extensions
{
    internal static class HttpResponseExtensions
    {
        internal static PagedOk<T> ToPagedOk<T>(this HttpResponse response, T items, PageInfo pageInfo) where T : IEnumerable
        {
            return new PagedOk<T>(items, pageInfo);
        }
    }
}

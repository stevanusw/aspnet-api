﻿using Microsoft.AspNetCore.Http;
using TodoApp.Api.ActionResults;
using TodoApp.Models.Paging;

namespace TodoApp.Api.Extensions
{
    internal static class HttpResponseExtensions
    {
        internal static PagedOk<T> ToPagedOk<T>(this HttpResponse response, T items, PageInfo pageInfo)
        {
            return new PagedOk<T>(items, pageInfo);
        }
    }
}

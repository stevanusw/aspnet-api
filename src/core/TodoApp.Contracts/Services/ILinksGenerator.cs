using Microsoft.AspNetCore.Http;
using System.Dynamic;

namespace TodoApp.Contracts.Services
{
    public interface ILinksGenerator<T>
    {
        IEnumerable<ExpandoObject> TryGenerateLinks(IEnumerable<T> dtos, string? fields, HttpContext httpContext);
    }
}

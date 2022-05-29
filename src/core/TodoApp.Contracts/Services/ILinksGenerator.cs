using Microsoft.AspNetCore.Http;
using TodoApp.Models.Links;

namespace TodoApp.Contracts.Services
{
    public interface ILinksGenerator<T>
    {
        LinkResponse TryGenerateLinks(IEnumerable<T> dtos, string? fields, HttpContext httpContext);
    }
}

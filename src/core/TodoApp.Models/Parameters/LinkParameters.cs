using Microsoft.AspNetCore.Http;

namespace TodoApp.Models.Parameters
{
    public record LinkParameters(RequestParameters RequestParameters, HttpContext HttpContext);
}

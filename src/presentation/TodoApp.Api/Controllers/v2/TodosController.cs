using Microsoft.AspNetCore.Mvc;
using TodoApp.Api.Extensions;
using TodoApp.Contracts.Services;
using TodoApp.Models.Parameters;

namespace TodoApp.Api.Controllers.V2
{
    [ApiController]
    [Route("api/v{v:apiVersion}/todos")]
    [ApiVersion("2.0")]
    public class TodosController : ControllerBase
    {
        private readonly IServiceManager _services;

        public TodosController(IServiceManager services)
        {
            _services = services;
        }

        [HttpGet]
        [MapToApiVersion("2.0")]
        public async Task<IActionResult> GetTodos([FromQuery] TodoParameters parameters)
        {
            var linkParameters = new LinkParameters(parameters, HttpContext);
            var model = await _services.Todo.GetTodosAsync(linkParameters);
            var v2Model = model.Dto.ShapedDtos!.Select(d => ((dynamic)d).Name += " V2");

            return Response.ToPagedOk(v2Model, model.PageInfo);
        }
    }
}

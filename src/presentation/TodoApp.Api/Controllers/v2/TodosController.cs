using Asp.Versioning;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TodoApp.Api.Extensions;
using TodoApp.Contracts.Services;
using TodoApp.Models.Dtos;
using TodoApp.Models.Parameters;

namespace TodoApp.Api.Controllers
{
    [ApiController]
    [Route("api/v{v:apiVersion}/todos")]
    [ApiVersion("2.0")]
    public class TodosV2Controller : ControllerBase
    {
        private readonly IServiceManager _services;

        public TodosV2Controller(IServiceManager services)
        {
            _services = services;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TodoDto>))]
        public async Task<IActionResult> GetTodos([FromQuery] TodoParameters parameters)
        {
            var linkParameters = new LinkParameters(parameters);
            var model = await _services.Todo.GetTodosAsync(linkParameters);
            var v2Model = model.Dto.ShapedDtos!.Select(d => ((dynamic)d).Name += " V2");

            return Response.ToPagedOk(v2Model, model.PageInfo);
        }
    }
}

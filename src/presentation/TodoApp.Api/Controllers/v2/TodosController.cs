﻿using Microsoft.AspNetCore.Mvc;
using TodoApp.Api.Extensions;
using TodoApp.Contracts.Services;
using TodoApp.Models.Parameters;

namespace TodoApp.Api.Controllers.v2
{
    [ApiController]
    [Route("api/{v:apiVersion}/todos")]
    [ApiVersion("2.0")]
    [ApiExplorerSettings(GroupName = "v2")]
    // Without V2, api-supported-versions returns 1.0, 2.0
    public class TodosV2Controller : ControllerBase
    {
        private readonly IServiceManager _services;

        public TodosV2Controller(IServiceManager services)
        {
            _services = services;
        }

        [HttpGet]
        public async Task<IActionResult> GetTodos([FromQuery] TodoParameters parameters)
        {
            var linkParameters = new LinkParameters(parameters, HttpContext);
            var model = await _services.Todo.GetTodosAsync(linkParameters);
            var v2Model = model.Dto.ShapedDtos!.Select(d => ((dynamic)d).Name += " V2");

            return Response.ToPagedOk(v2Model, model.PageInfo);
        }
    }
}

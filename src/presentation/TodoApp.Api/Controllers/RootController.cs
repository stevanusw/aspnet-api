using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using TodoApp.Api.Controllers.V1;
using TodoApp.Models.Links;

namespace TodoApp.Api.Controllers
{
    [ApiController]
    [Route("api")]
    public class RootController : ControllerBase
    {
        private readonly LinkGenerator _linkGenerator;

        public RootController(LinkGenerator linkGenerator) => _linkGenerator = linkGenerator;
        
        [HttpGet(Name = "GetRoot")]
        public IActionResult GetRoot([FromHeader(Name = "Accept")] string mediaType = "")
        {
            //if (mediaType.Contains("application/vnd.todoapp.apiroot+json"))
            {
                var links = new List<Link>
                {
                    new Link(_linkGenerator.GetUriByName(HttpContext, nameof(GetRoot), null)!,
                        "self",
                        "GET"),
                    new Link(_linkGenerator.GetUriByName(HttpContext, nameof(TodosController.GetTodos), new
                    {
                        v = "1"
                    })!,
                        "todos",
                        "GET"),
                    new Link(_linkGenerator.GetUriByName(HttpContext, nameof(TodosController.CreateTodo), new
                    {
                        v = "1"
                    })!,
                        "create_todo",
                        "POST")
                };

                return Ok(links);
            }

           // return NoContent();
        }
    }
}

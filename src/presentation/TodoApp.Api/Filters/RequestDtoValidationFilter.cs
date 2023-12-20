using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace TodoApp.Api.Filters
{
    public class RequestDtoValidationFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (!context.ModelState.IsValid)
            {
                context.Result = new UnprocessableEntityObjectResult(new ValidationProblemDetails(context.ModelState)
                {
                    Type = $"https://datatracker.ietf.org/doc/html/rfc4918#section-11.2",
                    Status = StatusCodes.Status422UnprocessableEntity,
                    Instance = context.HttpContext.Request.Path,
                });
            }
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var requestDto = context.ActionArguments
                .SingleOrDefault(a => a.Value!.ToString()!.Contains("Dto")).Value;

            if (requestDto == null)
            {
                context.Result = new BadRequestObjectResult(new ValidationProblemDetails(context.ModelState)
                {
                    Type = $"https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1",
                    Status = StatusCodes.Status400BadRequest,
                    Instance = context.HttpContext.Request.Path,
                });

                return;
            }

            if (!context.ModelState.IsValid)
            {
                context.Result = new UnprocessableEntityObjectResult(new ValidationProblemDetails(context.ModelState)
                {
                    Type = $"https://datatracker.ietf.org/doc/html/rfc4918#section-11.2",
                    Status = StatusCodes.Status422UnprocessableEntity,
                    Instance = context.HttpContext.Request.Path,
                });
            }
        }
    }
}

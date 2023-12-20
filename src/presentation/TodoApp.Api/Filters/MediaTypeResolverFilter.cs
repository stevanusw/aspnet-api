using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Net.Http.Headers;

namespace TodoApp.Api.Filters
{
    public class MediaTypeResolverFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
			var mediaType = context.HttpContext.Request.Headers["Accept"].FirstOrDefault();

			if (MediaTypeHeaderValue.TryParse(mediaType, out MediaTypeHeaderValue? outMediaType))
			{
                context.HttpContext.Items.Add("AcceptHeaderMediaType", outMediaType);
            }
		}
    }
}

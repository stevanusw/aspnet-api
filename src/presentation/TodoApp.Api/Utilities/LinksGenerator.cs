using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Net.Http.Headers;
using System.Dynamic;
using TodoApp.Api.Controllers;
using TodoApp.Contracts.Services;
using TodoApp.Models.Dtos;
using TodoApp.Models.Links;

namespace TodoApp.Api.Utilities
{
    internal class LinksGenerator<T> : ILinksGenerator<T>
    {
		private readonly LinkGenerator _linkGenerator;
		private readonly IDataShaper<T> _dataShaper;

		public LinksGenerator(LinkGenerator linkGenerator, IDataShaper<T> dataShaper)
		{
			_linkGenerator = linkGenerator;
			_dataShaper = dataShaper;
		}

		public LinkResponse TryGenerateLinks(IEnumerable<T> dtos, string? fields, HttpContext httpContext)
		{
			var shapedDtos = _dataShaper.Shape(dtos, fields);

			if (ShouldGenerateLinks(httpContext))
			{
				return ReturnLinkedDtos(shapedDtos, fields, httpContext);
			}

			return ReturnShapedDtos(shapedDtos);
		}

		private bool ShouldGenerateLinks(HttpContext httpContext)
		{
			var mediaType = httpContext.Items["AcceptHeaderMediaType"] as MediaTypeHeaderValue;
			if (mediaType == null)
            {
				return false;
            }

			return mediaType.SubTypeWithoutSuffix.EndsWith("hateoas", StringComparison.InvariantCultureIgnoreCase);
		}

		private LinkResponse ReturnShapedDtos(IEnumerable<ShapedDto> shapedDtos) =>
			new LinkResponse
			{
				ShapedDtos = shapedDtos.Select(d => d.Dto)
			};

		private LinkResponse ReturnLinkedDtos(IEnumerable<ShapedDto> shapedDtos, string? fields, HttpContext httpContext)
		{
			var list = shapedDtos.ToList();

			for (var i = 0; i < list.Count(); i++)
			{
				var dtoLinks = typeof(T).Name switch
                {
					nameof(TodoDto) => CreateLinksForTodo(httpContext, list[i].Id, fields),
					nameof(TaskDto) => CreateLinksForTask(httpContext, list[i].Id, fields),
					_ => throw new ArgumentException($"Unknown type {typeof(T).Name}")
                };

				list[i].Dto.TryAdd("Links", dtoLinks);
			}

			var dtos = list.Select(d => d.Dto);
			var links = typeof(T).Name switch
			{
				nameof(TodoDto) => CreateLinksForTodos(httpContext, fields),
				nameof(TaskDto) => CreateLinksForTasks(httpContext, fields),
				_ => throw new ArgumentException($"Unknown type {typeof(T).Name}")
			};

			return new LinkResponse
			{
				HasLinks = true,
				LinkedDtos = new LinkCollectionWrapper<ExpandoObject>(dtos, links)
			};
		}

		private IEnumerable<Link> CreateLinksForTodo(HttpContext httpContext, int id, string? fields)
		{
			fields ??= "";

			var links = new List<Link>
			{
				new Link(_linkGenerator.GetUriByAction(httpContext, nameof(TodosController.GetTodo), values: new { id, fields })!,
					"self",
					"GET"),

				new Link(_linkGenerator.GetUriByAction(httpContext, nameof(TodosController.DeleteTodo), values: new { id })!,
					"delete_todo",
					"DELETE"),

				new Link(_linkGenerator.GetUriByAction(httpContext, nameof(TodosController.UpdateTodo), values: new { id })!,
					"update_todo",
					"PUT"),

				new Link(_linkGenerator.GetUriByAction(httpContext, nameof(TodosController.PartiallyUpdateTodo), values: new { id })!,
					"partially_update_todo",
					"PATCH"),
			};

			return links;
		}

		private IEnumerable<Link> CreateLinksForTodos(HttpContext httpContext, string? fields)
		{
			fields ??= "";

			var links = new List<Link>
			{
				new Link(_linkGenerator.GetUriByAction(httpContext, nameof(TodosController.GetTodos), values: new { fields })!,
					"self",
					"GET"),

				new Link(_linkGenerator.GetUriByAction(httpContext, nameof(TodosController.CreateTodo))!,
					"create_todo",
					"POST"),
			};

			return links;
		}

		private IEnumerable<Link> CreateLinksForTask(HttpContext httpContext, int taskId, string? fields)
		{
			fields ??= "";

			var todoId = int.Parse(httpContext.GetRouteValue("todoId")!.ToString()!);
			var links = new List<Link>
			{
				new Link(_linkGenerator.GetUriByAction(httpContext, nameof(TasksController.GetTask), values: new { todoId, taskId, fields })!,
					"self",
					"GET"),

				new Link(_linkGenerator.GetUriByAction(httpContext, nameof(TasksController.DeleteTask), values: new { todoId, taskId })!,
					"delete_task",
					"DELETE"),

				new Link(_linkGenerator.GetUriByAction(httpContext, nameof(TasksController.UpdateTask), values: new { todoId, taskId })!,
					"update_task",
					"PUT")
			};

			return links;
		}

		private IEnumerable<Link> CreateLinksForTasks(HttpContext httpContext, string? fields)
		{
			fields ??= "";

			var todoId = int.Parse(httpContext.GetRouteValue("todoId")!.ToString()!);
			var links = new List<Link>
			{
				new Link(_linkGenerator.GetUriByAction(httpContext, nameof(TasksController.GetTasks), values: new { todoId, fields })!,
					"self",
					"GET"),

				new Link(_linkGenerator.GetUriByAction(httpContext, nameof(TasksController.CreateTask), values: new { todoId })!,
					"create_task",
					"POST")
			};

			return links;
		}
	}
}

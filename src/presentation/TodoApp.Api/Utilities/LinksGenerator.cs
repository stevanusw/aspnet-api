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

		public IEnumerable<ExpandoObject> TryGenerateLinks(IEnumerable<T> dtos, string? fields, HttpContext httpContext)
		{
			var shapedDtos = _dataShaper.Shape(dtos, fields);

			if (ShouldGenerateLinks(httpContext))
			{
				return ReturnLinkedDtos(shapedDtos, fields, httpContext);
			}

			return ReturnShapedEntities(shapedDtos);
		}

		private bool ShouldGenerateLinks(HttpContext httpContext)
		{
			//var mediaType = (MediaTypeHeaderValue)httpContext.Items["AcceptHeaderMediaType"]!;

			//return mediaType.SubTypeWithoutSuffix.EndsWith("hateoas", StringComparison.InvariantCultureIgnoreCase);

			return true;
		}

		private IEnumerable<ExpandoObject> ReturnShapedEntities(IEnumerable<ShapedDto> shapedDtos) => shapedDtos.Select(d => d.Entity);

		private IEnumerable<ExpandoObject> ReturnLinkedDtos(IEnumerable<ShapedDto> shapedDtos, string? fields, HttpContext httpContext)
		{
			var list = shapedDtos.ToList();

			for (var i = 0; i < list.Count(); i++)
			{
				var links = typeof(T).Name switch
                {
					nameof(TodoDto) => CreateLinksForTodo(httpContext, list[i].Id, fields),
					_ => throw new ArgumentException($"Unknown type {typeof(T).Name}")
                };

				list[i].Entity.TryAdd("Links", links);
			}

			var entities = list.Select(d => d.Entity);

			return entities;
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
					"PATCH")
			};

			return links;
		}
    }
}

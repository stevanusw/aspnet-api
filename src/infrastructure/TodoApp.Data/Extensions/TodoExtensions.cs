using System.Linq.Dynamic.Core;
using TodoApp.Data.Extensions.Utilities;
using TodoApp.Entities;

namespace TodoApp.Data.Extensions
{
    internal static class TodoExtensions
    {
        internal static IQueryable<Todo> Filter(this IQueryable<Todo> todos, bool? isCompleted)
        {
            if (isCompleted == null)
            {
                return todos;
            }

            return todos.Where(t => t.IsCompleted == isCompleted);
        }

        internal static IQueryable<Todo> Search(this IQueryable<Todo> todos, string? query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return todos;
            }

            return todos.Where(t => t.Name!.ToLower().Contains(query.Trim().ToLower()));
        }

        internal static IQueryable<Todo> Sort(this IQueryable<Todo> todos, string? query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return todos;
            }

            var orderByQuery = QueryBuilder.CreateOrderByQuery<Todo>(query);
            if (string.IsNullOrWhiteSpace(orderByQuery))
            {
                return todos;
            }

            return todos.OrderBy(orderByQuery);
        }
    }
}

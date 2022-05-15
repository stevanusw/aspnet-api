using System.Linq.Dynamic.Core;
using TodoApp.Data.Extensions.Utilities;

namespace TodoApp.Data.Extensions
{
    internal static class TaskExtensions
    {
        internal static IQueryable<Entities.Task> Search(this IQueryable<Entities.Task> tasks, string? query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return tasks;
            }

            return tasks.Where(t => t.Name!.ToLower().Contains(query.Trim().ToLower()));
        }

        internal static IQueryable<Entities.Task> Sort(this IQueryable<Entities.Task> tasks, string? query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return tasks;
            }

            var orderByQuery = QueryBuilder.CreateOrderByQuery<Entities.Task>(query);
            if (string.IsNullOrWhiteSpace(orderByQuery))
            {
                return tasks;
            }

            return tasks.OrderBy(orderByQuery);
        }
    }
}

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

        internal static IQueryable<Todo> Search(this IQueryable<Todo> todos, string? searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return todos;
            }

            return todos.Where(t => t.Name!.ToLower().Contains(searchTerm.Trim().ToLower()));
        }
    }
}

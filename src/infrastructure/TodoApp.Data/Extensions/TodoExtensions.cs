namespace TodoApp.Data.Extensions
{
    internal static class TaskExtensions
    {
        internal static IQueryable<Entities.Task> Search(this IQueryable<Entities.Task> tasks, string? searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return tasks;
            }

            return tasks.Where(t => t.Name!.ToLower().Contains(searchTerm.Trim().ToLower()));
        }
    }
}

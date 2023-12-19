using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace TodoApp.Data
{
    internal class TodoDbContextFactory : IDesignTimeDbContextFactory<TodoDbContext>
    {
        public TodoDbContext CreateDbContext(string[] args)
        {
            var connectionString = @"Server=(localdb)\mssqllocaldb;Database=Todo;Integrated Security=true";
            var builder = new DbContextOptionsBuilder<TodoDbContext>()
            .UseSqlServer(connectionString)
            .EnableSensitiveDataLogging();
            
            Console.WriteLine(connectionString);

            return new TodoDbContext(builder.Options);
        }
    }
}

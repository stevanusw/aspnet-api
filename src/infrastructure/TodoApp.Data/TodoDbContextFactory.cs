using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace TodoApp.Data
{
    public class TodoDbContextFactory : IDesignTimeDbContextFactory<TodoDbContext>
    {
        public TodoDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder();
            var connectionString = @"Server=(localdb)\mssqllocaldb;Database=Todo;Integrated Security=true;";
            optionsBuilder.UseSqlServer(connectionString);
            Console.WriteLine(connectionString);

            return new TodoDbContext(optionsBuilder.Options);
        }
    }
}

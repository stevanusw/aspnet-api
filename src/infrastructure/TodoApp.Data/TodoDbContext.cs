using Microsoft.EntityFrameworkCore;
using TodoApp.Domain.Entities;

namespace TodoApp.Data
{
    public class TodoDbContext : DbContext
    {
        public TodoDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.ApplyConfigurationsFromAssembly(typeof(BaseEntityConfiguration).Assembly);

            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                if (entity.ClrType.IsSubclassOf(typeof(BaseEntity)))
                {
                    entity.FindProperty(nameof(BaseEntity.CreateDate))!
                        .SetDefaultValueSql("SYSUTCDATETIME()");

                    entity.FindProperty(nameof(BaseEntity.LastUpdateDate))
                        .SetComputedColumnSql("SYSUTCDATETIME()");
                }
            }
        }

        public DbSet<Todo>? TodoList { get; set; }
        public DbSet<Domain.Entities.Task>? Tasks { get; set; }
    }
}

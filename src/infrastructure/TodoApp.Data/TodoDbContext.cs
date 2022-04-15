using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using TodoApp.Data.Configuration;
using TodoApp.Domain.Entities;

namespace TodoApp.Data
{
    internal class TodoDbContext : DbContext
    {
        public TodoDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TodoConfiguration).Assembly);

            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                if (entity.ClrType.IsSubclassOf(typeof(BaseEntity)))
                {
                    entity.FindProperty(nameof(BaseEntity.Id))!
                        .IsPrimaryKey();

                    entity.FindProperty(nameof(BaseEntity.CreateDate))!
                        .SetDefaultValueSql("SYSUTCDATETIME()");

                    entity.FindProperty(nameof(BaseEntity.LastUpdateDate))!
                        .SetComputedColumnSql("SYSUTCDATETIME()");

                    entity.FindProperty(nameof(BaseEntity.Timestamp))!
                        .IsConcurrencyToken = true;

                    entity.FindProperty(nameof(BaseEntity.Timestamp))!
                        .ValueGenerated = ValueGenerated.OnAddOrUpdate;
                }
            }
        }

        public DbSet<Todo>? TodoList { get; set; }
        public DbSet<Domain.Entities.Task>? Tasks { get; set; }
    }
}

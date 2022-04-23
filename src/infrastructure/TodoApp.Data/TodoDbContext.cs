using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using TodoApp.Data.Configuration;
using TodoApp.Entities;

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

                    entity.FindProperty(nameof(BaseEntity.CreateDate))!
                        .ValueGenerated = ValueGenerated.OnAdd;

                    entity.FindProperty(nameof(BaseEntity.LastUpdateDate))!
                        .SetDefaultValueSql("SYSUTCDATETIME()");

                    entity.FindProperty(nameof(BaseEntity.LastUpdateDate))!
                        .ValueGenerated = ValueGenerated.OnAddOrUpdate;

                    entity.FindProperty(nameof(BaseEntity.Timestamp))!
                        .IsConcurrencyToken = true;

                    entity.FindProperty(nameof(BaseEntity.Timestamp))!
                        .ValueGenerated = ValueGenerated.OnAddOrUpdate;
                }
            }
        }

        public DbSet<Todo>? Todos { get; set; }
        public DbSet<Entities.Task>? Tasks { get; set; }
        public DbSet<Log>? Logs { get; set; }
    }
}

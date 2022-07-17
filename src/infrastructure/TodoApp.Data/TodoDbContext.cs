using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using TodoApp.Data.Configuration;
using TodoApp.Entities;

namespace TodoApp.Data
{
    internal class TodoDbContext : IdentityDbContext<User>
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

                    entity.FindProperty(nameof(BaseEntity.CreateDateUtc))!
                        .SetDefaultValueSql("SYSUTCDATETIME()");

                    entity.FindProperty(nameof(BaseEntity.CreateDateUtc))!
                        .ValueGenerated = ValueGenerated.OnAdd;

                    entity.FindProperty(nameof(BaseEntity.LastUpdateDateUtc))!
                        .SetDefaultValueSql("SYSUTCDATETIME()");

                    entity.FindProperty(nameof(BaseEntity.LastUpdateDateUtc))!
                        .ValueGenerated = ValueGenerated.OnAdd;

                    entity.FindProperty(nameof(BaseEntity.Timestamp))!
                        .IsConcurrencyToken = true;

                    entity.FindProperty(nameof(BaseEntity.Timestamp))!
                        .ValueGenerated = ValueGenerated.OnAddOrUpdate;
                }
            }
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var modifiedEntities = ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Modified && e.Entity is BaseEntity)
                .Select(e => (BaseEntity)e.Entity);

            foreach (var e in modifiedEntities)
            {
                e.LastUpdateDateUtc = DateTime.UtcNow;
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        public DbSet<Todo>? Todos { get; set; }
        public DbSet<Entities.Task>? Tasks { get; set; }
        public DbSet<Log>? Logs { get; set; }
    }
}

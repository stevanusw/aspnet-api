using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TodoApp.Entities;

namespace TodoApp.Data.Configuration
{
    internal class TodoConfiguration : IEntityTypeConfiguration<Todo>
    {
        public void Configure(EntityTypeBuilder<Todo> builder)
        {
            builder.ToTable("Todos", "dbo");

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasMany(p => p.Tasks)
                .WithOne(d => d.TodoNavigation)
                .HasForeignKey(d => d.TodoId)
                .OnDelete(DeleteBehavior.Cascade);

            #region Seed
            for (var i = 1; i <= 100; i++)
            {
                builder.HasData(new Todo
                {
                    Id = i,
                    Name = Guid.NewGuid().ToString(),
                });
            }
            #endregion
        }
    }
}

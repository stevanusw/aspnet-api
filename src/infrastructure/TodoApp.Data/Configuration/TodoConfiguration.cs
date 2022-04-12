using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TodoApp.Domain.Entities;

namespace TodoApp.Data.Configuration
{
    internal class TodoConfiguration : IEntityTypeConfiguration<Todo>
    {
        public void Configure(EntityTypeBuilder<Todo> builder)
        {
            builder.ToTable("TodoList", "dbo");

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasMany(p => p.Tasks)
                .WithOne(d => d.TodoNavigation)
                .HasForeignKey(d => d.TodoId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

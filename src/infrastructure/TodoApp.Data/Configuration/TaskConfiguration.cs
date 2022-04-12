using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TodoApp.Data.Configuration
{
    internal class TaskConfiguration : IEntityTypeConfiguration<Domain.Entities.Task>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.Task> builder)
        {
            builder.ToTable("Tasks", "dbo");

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasOne(d => d.TodoNavigation)
                .WithMany(p => p.Tasks)
                .HasForeignKey(d => d.TodoId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TodoApp.Data.Configuration
{
    internal class TaskConfiguration : IEntityTypeConfiguration<Entities.Task>
    {
        public void Configure(EntityTypeBuilder<Entities.Task> builder)
        {
            builder.ToTable("Tasks", "dbo");

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasOne(d => d.TodoNavigation)
                .WithMany(p => p.Tasks)
                .HasForeignKey(d => d.TodoId)
                .OnDelete(DeleteBehavior.Cascade);

            #region Seed
            for (var i = 1; i <= 100; i++)
            {
                for (int j = 1; j <= 50; j++)
                {
                    builder.HasData(new Entities.Task
                    {
                        Id = j + (i - 1) * 50,
                        TodoId = i,
                        Name = Guid.NewGuid().ToString(),
                    });
                }
            }
            #endregion
        }
    }
}

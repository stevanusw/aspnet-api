using Entities = TodoApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TodoApp.Data.Configuration
{
    internal class TaskConfiguration : IEntityTypeConfiguration<Entities.Task>
    {
        public void Configure(EntityTypeBuilder<Entities.Task> builder)
        {
            builder.ToTable("Tasks", "dbo");

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasOne(d => d.TodoNavigation)
                .WithMany(p => p.Tasks)
                .HasForeignKey(d => d.TodoId)
                .OnDelete(DeleteBehavior.Cascade);

            #region Seed
            builder.HasData(new Entities.Task
            {
                Id = 1,
                TodoId = 1,
                Name = "Learn Ecmascript",
            },
            new Entities.Task
            {
                Id = 2,
                TodoId = 1,
                Name = "Learn Typescript",
            },
            new Entities.Task
            {
                Id = 3,
                TodoId = 2,
                Name = "Learn C#",
            },
            new Entities.Task
            {
                Id = 4,
                TodoId = 2,
                Name = "Learn SQL",
            }); 
            #endregion
        }
    }
}

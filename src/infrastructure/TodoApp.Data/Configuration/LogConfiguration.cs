using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TodoApp.Entities;

namespace TodoApp.Data.Configuration
{
    internal class LogConfiguration : IEntityTypeConfiguration<Log>
    {
        public void Configure(EntityTypeBuilder<Log> builder)
        {
            //builder.ToTable("Logs", "dbo");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .UseIdentityColumn();

            builder.Property(e => e.Level)
                .HasMaxLength(128);

            builder.Property(e => e.Properties)
                .HasColumnType("xml");
        }
    }
}

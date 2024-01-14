using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TodoApp.Data.Configuration
{
    internal class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(new IdentityRole
            {
                //Id = "2b07d854-aaae-41aa-9b19-e9c63feafd85",
                Name = "Member",
                NormalizedName = "MEMBER"
            },
            new IdentityRole
            {
                //Id = "40c8001d-6cc3-4219-8415-b1919e4035d3",
                Name = "Administrator",
                NormalizedName = "ADMINISTRATOR"
            });
        }
    }
}

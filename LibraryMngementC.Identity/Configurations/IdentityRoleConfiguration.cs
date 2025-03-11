using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryMngementC.Identity.Configurations
{
    public class IdentityRoleConfiguration
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
                new IdentityRole()
                {
                    Name = "Admin",
                    ConcurrencyStamp = "1",
                    NormalizedName = "LibraryManagementStaff",
                },
                new IdentityRole()
                {
                    Name = "User",
                    ConcurrencyStamp = "2",
                    NormalizedName = "LibraryStaffMinor",
                }
            );
        }
    }
}

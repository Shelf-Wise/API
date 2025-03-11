using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LibraryMngementC.Identity
{
    public class IdentityDatabaseContext : IdentityDbContext<IdentityUser>
    {
        public IdentityDatabaseContext(DbContextOptions<IdentityDatabaseContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            SeedRoles(builder);
        }

        private void SeedRoles(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<IdentityRole>()
                .HasData( 
                    new IdentityRole()
                    {
                        Name = "LibraryStaffManagement",
                        ConcurrencyStamp = "1",
                        NormalizedName = "LibraryStaffManagement",
                    },
                    new IdentityRole()
                    {
                        Name = "LibraryStaffMinor",
                        ConcurrencyStamp = "2",
                        NormalizedName = "LibraryStaffMinor",
                    },
                    new IdentityRole()
                    {
                        Name = "LibraryMember",
                        ConcurrencyStamp = "3",
                        NormalizedName = "LibraryMember",
                    }
                );
        }
    }
}

using LibraryManagementC.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementC.Persistance
{
    public class ApplicationWriteDbContext : DbContext
    {
        public ApplicationWriteDbContext(DbContextOptions<ApplicationWriteDbContext> options)
            : base(options) { }

        public DbSet<Book> Books { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Librarian> Librarians { get; set; }
        public DbSet<Fine> Fine { get; set; }
        public DbSet<Genre> Genre { get; set; }
        public DbSet<BorrowHistory> BorrowHistories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(ApplicationWriteDbContext).Assembly,
                WriteConfigurationFilter
            );
        }

        private static bool WriteConfigurationFilter(Type type) =>
            type.FullName?.Contains("Configurations.Write") ?? false;
    }
}

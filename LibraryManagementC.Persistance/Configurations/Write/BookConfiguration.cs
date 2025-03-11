using LibraryManagementC.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManagementC.Persistance.Configurations.Write
{
    internal sealed class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasKey(x => x.Id);

            builder
                .HasOne(b => b.Member)
                .WithMany(m => m.Books)
                .HasForeignKey(b => b.MemberId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Property(b => b.Title).IsRequired().HasMaxLength(255);

            builder.Property(b => b.Author).IsRequired().HasMaxLength(255);

            builder.Property(b => b.MemberId).IsRequired(false);

            BaseDomainAuditableEntityConfiguration.Configure<Book>(builder);
        }
    }
}

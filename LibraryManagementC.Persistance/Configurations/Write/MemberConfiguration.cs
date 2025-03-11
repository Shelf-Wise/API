using LibraryManagementC.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManagementC.Persistance.Configurations.Write
{
    public class MemberConfiguration : IEntityTypeConfiguration<Member>
    {
        public void Configure(EntityTypeBuilder<Member> builder)
        {
            builder.HasKey(x => x.Id);

            BaseDomainAuditableEntityConfiguration.Configure<Member>(builder);
        }
    }
}

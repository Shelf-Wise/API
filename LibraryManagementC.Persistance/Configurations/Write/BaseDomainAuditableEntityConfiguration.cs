using LibraryManagementC.Domain.Primitives;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManagementC.Persistance.Configurations.Write
{
    public static class BaseDomainAuditableEntityConfiguration
    {
        public static void Configure<TEntity>(EntityTypeBuilder<TEntity> builder) where TEntity : BaseDomainAuditableEntity
        {
            builder.Property(entity => entity.CreatedBy)
                .IsRequired()
                .HasColumnType("varchar")
                .HasMaxLength(200);

            builder.Property(entity => entity.CreatedAt)
                .IsRequired()
                .HasColumnType("timestamptz");
            
            builder.Property(entity => entity.UpdatedBy)
                .IsRequired()
                .HasColumnType("varchar")
                .HasMaxLength(200);

            builder.Property(entity => entity.UpdateAt)
                .IsRequired()
                .HasColumnType("timestamptz");
        }
    }
}

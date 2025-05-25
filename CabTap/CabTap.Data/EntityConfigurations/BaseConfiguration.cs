using CabTap.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CabTap.Data.EntityConfigurations;

public class BaseConfiguration : IEntityTypeConfiguration<BaseEntity>
{

    public void Configure(EntityTypeBuilder<BaseEntity> baseEntity)
    {
        baseEntity.Property(b => b.CreatedOn)
            .IsRequired();

        baseEntity.Property(b => b.LastModifiedOn)
            .IsRequired();

        baseEntity.Property(b => b.CreatedBy)
            .IsRequired()
            .HasMaxLength(50);

        baseEntity.Property(b => b.LastModifiedBy)
            .IsRequired()
            .HasMaxLength(50);
    }
}
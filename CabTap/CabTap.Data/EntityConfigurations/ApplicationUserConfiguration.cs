using CabTap.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CabTap.Data.EntityConfigurations;

public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> user)
    {
        user.Property(u => u.FirstName)
            .IsRequired()
            .HasMaxLength(50);
        
        user.Property(u => u.LastName)
            .IsRequired()
            .HasMaxLength(50);

        user.Property(u => u.Address)
            .IsRequired()
            .HasMaxLength(250);
    }
}
using CabTap.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CabTap.Data.EntityConfigurations;

public class DriverConfiguration : IEntityTypeConfiguration<Driver>
{
    public void Configure(EntityTypeBuilder<Driver> driver)
    {
        driver.HasKey(d => d.Id);
        driver.Property(d => d.Id)
            .HasMaxLength(50)
            .IsRequired()
            .ValueGeneratedOnAdd();
            
        driver.Property(d => d.Name)
            .IsRequired()
            .HasMaxLength(50);
            
        driver.HasMany(d => d.Taxis)
            .WithOne(t => t.Driver)
            .HasForeignKey(t => t.DriverId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
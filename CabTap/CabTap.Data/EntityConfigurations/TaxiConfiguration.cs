using CabTap.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CabTap.Data.EntityConfigurations;

public class TaxiConfiguration : IEntityTypeConfiguration<Taxi>
{
    public void Configure(EntityTypeBuilder<Taxi> taxi)
    {
        taxi.HasKey(t => t.Id);
        
        taxi.HasIndex(c => c.RegNumber)
            .IsUnique();
        
        taxi.Property(c => c.RegNumber)
            .IsRequired()
            .HasMaxLength(8);

        taxi.Property(t => t.ManufacturerId)
            .IsRequired();
        taxi.HasOne(t => t.Manufacturer)
            .WithMany(m => m.Taxis)
            .HasForeignKey(t => t.ManufacturerId)
            .OnDelete(DeleteBehavior.Restrict);
        
        taxi.Property(t => t.CategoryId)
            .IsRequired();
        taxi.HasOne(t => t.Category)
            .WithMany(c => c.Taxis)
            .HasForeignKey(t => t.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        taxi.Property(t => t.DriverId)
            .IsRequired()
            .HasMaxLength(50);
        taxi.HasOne(t => t.Driver)
            .WithMany(d => d.Taxis)
            .HasForeignKey(t => t.DriverId)
            .OnDelete(DeleteBehavior.Restrict);
        
        taxi.Property(t => t.Description)
            .HasMaxLength(100);

        taxi.Property(t => t.Picture)
            .HasMaxLength(500);
        
        taxi.Property(t => t.TaxiStatus)
            .IsRequired();

        taxi.Property(t => t.PassengerSeats)
            .IsRequired();

        taxi.HasMany(t => t.Reservations)
            .WithOne(r => r.Taxi)
            .HasForeignKey(r => r.TaxiId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
using CabTap.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CabTap.Data.EntityConfigurations;

public class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
{
    public void Configure(EntityTypeBuilder<Reservation> reservation)
    {
        reservation.HasKey(r => r.Id);

        reservation.Property(r => r.Id)
            .HasMaxLength(50)
            .IsRequired()
            .ValueGeneratedOnAdd();

        reservation.Property(r => r.UserId)
            .IsRequired()
            .HasMaxLength(50);

        reservation.Property(r => r.TaxiId)
            .IsRequired();

        reservation.HasIndex(r => r.ReservationDateTime);
        reservation.Property(r => r.ReservationDateTime)
            .IsRequired();

        reservation.Property(r => r.Origin)
            .IsRequired()
            .HasMaxLength(250);

        reservation.Property(r => r.Destination)
            .IsRequired()
            .HasMaxLength(250);

        reservation.Property(r => r.OriginPoint)
            .HasColumnType("geometry (Point, 4326)")
            .IsRequired();

        reservation.Property(r => r.DestinationPoint)
            .HasColumnType("geometry (Point, 4326)")
            .IsRequired();

        reservation.Property(r => r.ReservationType)
            .IsRequired();

        reservation.HasIndex(r => r.Duration);
        reservation.Property(r => r.Duration)
            .IsRequired();

        reservation.Property(r => r.Distance)
            .IsRequired();

        reservation.HasIndex(r => r.Price);
        reservation.Property(r => r.Price)
            .IsRequired()
            .HasPrecision(18, 2);

        reservation.Property(r => r.PassengersCount)
            .IsRequired();

        reservation.Property(r => r.RideStatus)
            .IsRequired();

        reservation.HasOne(r => r.User)
            .WithMany()
            .HasForeignKey(r => r.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        reservation.HasOne(r => r.Taxi)
            .WithMany(t => t.Reservations)
            .HasForeignKey(r => r.TaxiId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
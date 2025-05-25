using CabTap.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CabTap.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Taxi> Taxis { get; set; } = null!;
    public DbSet<Category> Categories { get; set; } = null!;
    public DbSet<Manufacturer> Manufacturers { get; set; } = null!;
    public DbSet<Driver> Drivers { get; set; } = null!;
    public DbSet<Reservation> Reservations { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.HasDefaultSchema("public");

        ConfigureTaxi(builder);
        ConfigureCategory(builder);
        ConfigureManufacturer(builder);
        ConfigureDriver(builder);
        ConfigureReservation(builder);
    }

    private static void ConfigureTaxi(ModelBuilder builder)
    {
        builder.Entity<Taxi>(taxi =>
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
        });
    }

    private static void ConfigureCategory(ModelBuilder builder)
    {
        builder.Entity<Category>(category =>
        {
            category.HasKey(c => c.Id);

            category.HasIndex(c => c.Name)
                .IsUnique();
            
            category.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(50);

            category.Property(c => c.Rate)
                .HasPrecision(18, 2)
                .IsRequired();
            
            category.Property(c => c.Image)
                .HasDefaultValue("/images/categories/default.png");
            
            category.HasMany(c => c.Taxis)
                .WithOne(t => t.Category)
                .HasForeignKey(t => t.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }

    private static void ConfigureManufacturer(ModelBuilder builder)
    {
        builder.Entity<Manufacturer>(manufacturer =>
        {
            manufacturer.HasKey(m => m.Id);

            manufacturer.HasIndex(c => c.Name)
                .IsUnique();

            manufacturer.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(50);

            manufacturer.HasMany(c => c.Taxis)
                .WithOne(t => t.Manufacturer)
                .HasForeignKey(t => t.ManufacturerId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }

    private static void ConfigureDriver(ModelBuilder builder)
    {
        builder.Entity<Driver>(driver =>
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
        });
    }

    private static void ConfigureReservation(ModelBuilder builder)
    {
        builder.Entity<Reservation>(reservation =>
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

            reservation.Property(r => r.Duration)
                .IsRequired();

            reservation.Property(r => r.Distance)
                .IsRequired();

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
        });
    }
}
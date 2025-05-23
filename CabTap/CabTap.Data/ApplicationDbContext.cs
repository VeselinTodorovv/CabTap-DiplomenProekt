using CabTap.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

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

        builder.Entity<Taxi>()
            .HasIndex(t => t.RegNumber)
            .IsUnique();
        
        builder.Entity<Reservation>()
            .Property(r => r.Price)
            .HasPrecision(18, 2);
        
        builder.Entity<Category>()
            .Property(c => c.Rate)
            .HasPrecision(18, 2);

        ConfigureDateTimeValueConverters(builder);
    }

    private static void ConfigureDateTimeValueConverters(ModelBuilder builder)
    {
        var utcDateTimeConverter = new ValueConverter<DateTime, DateTime>(
            v => v.Kind == DateTimeKind.Utc ? v : DateTime.SpecifyKind(v, DateTimeKind.Utc),
            v => DateTime.SpecifyKind(v, DateTimeKind.Utc)
        );

        var nullableUtcDateTimeConverter = new ValueConverter<DateTime?, DateTime?>(
            v => v.HasValue ? DateTime.SpecifyKind(v.Value, DateTimeKind.Utc) : v,
            v => v.HasValue ? DateTime.SpecifyKind(v.Value, DateTimeKind.Utc) : v
        );

        foreach (var entityType in builder.Model.GetEntityTypes())
        {
            foreach (var property in entityType.GetProperties()
                         .Where(p => p.ClrType == typeof(DateTime)))
            {
                property.SetValueConverter(utcDateTimeConverter);
            }

            var nullableDateTimeProperties = entityType.GetProperties()
                .Where(p => p.ClrType == typeof(DateTime?));
            
            foreach (var property in nullableDateTimeProperties)
            {
                property.SetValueConverter(nullableUtcDateTimeConverter);
            }
        }
    }
}
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

        builder.Entity<Taxi>()
            .HasIndex(t => t.RegNumber)
            .IsUnique();
        
        builder.Entity<Reservation>()
            .Property(r => r.Price)
            .HasPrecision(18, 2);
        
        builder.Entity<Category>()
            .Property(c => c.Rate)
            .HasPrecision(18, 2);
    }
}
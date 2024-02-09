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
        
    public DbSet<Taxi> Taxis { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Driver> Drivers { get; set; }
    public DbSet<Reservation> Reservations { get; set; }
}
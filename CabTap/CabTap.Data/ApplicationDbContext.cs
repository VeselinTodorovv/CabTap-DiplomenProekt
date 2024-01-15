using CabTap.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CabTap.Data
{
    public class ApplicationDbContext : DbContext
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
}
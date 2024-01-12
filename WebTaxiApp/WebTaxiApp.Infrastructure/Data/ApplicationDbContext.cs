using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using WebTaxiApp.Infrastructure.Data.Domain;

namespace WebTaxiApp.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {}

        DbSet<Driver> Drivers { get; set; }
        DbSet<Category> Categories { get; set; }
        DbSet<Reservation> Reservations { get; set; }
        DbSet<Taxi> Taxis { get; set; }
    }
}
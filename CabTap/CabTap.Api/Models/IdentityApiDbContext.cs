using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CabTap.Api.Models;

public class IdentityApiDbContext : IdentityDbContext<ApplicationUser>
{
    public IdentityApiDbContext(DbContextOptions<IdentityApiDbContext> options)
        : base(options)
    { }
}
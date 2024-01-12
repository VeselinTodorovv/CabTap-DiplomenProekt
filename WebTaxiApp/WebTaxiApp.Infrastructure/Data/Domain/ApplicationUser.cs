using Microsoft.AspNetCore.Identity;

namespace WebTaxiApp.Infrastructure.Data.Domain
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string Address { get; set; } = null!;
    }
}
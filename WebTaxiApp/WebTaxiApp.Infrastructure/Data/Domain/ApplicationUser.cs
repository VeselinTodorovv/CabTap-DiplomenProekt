using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Identity;

namespace WebTaxiApp.Infrastructure.Data.Domain
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string FirstName { get; set; } = null!;

        [Required]
        public string LastName { get; set; } = null!;

        [Required]
        public string Address { get; set; } = null!;
    }
}
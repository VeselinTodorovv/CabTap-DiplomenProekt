using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Identity;

namespace CabTap.Api.Models;

public class ApplicationUser : IdentityUser
{
    [Required]
    [StringLength(30, MinimumLength = 3)]
    public string FirstName { get; init; } = null!;

    [Required]
    [StringLength(30, MinimumLength = 3)]
    public string LastName { get; init; } = null!;

    [Required]
    [StringLength(50, MinimumLength = 3)]
    public string Address { get; init; } = null!;
}
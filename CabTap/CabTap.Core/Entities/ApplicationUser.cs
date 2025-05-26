using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace CabTap.Core.Entities;

public class ApplicationUser : IdentityUser
{
    [StringLength(30, MinimumLength = 3)]
    public string FirstName { get; init; } = null!;

    [StringLength(30, MinimumLength = 3)]
    public string LastName { get; init; } = null!;

    [StringLength(250, MinimumLength = 3)]
    public string Address { get; init; } = null!;
}

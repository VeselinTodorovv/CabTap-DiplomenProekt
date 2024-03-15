using System.ComponentModel.DataAnnotations;

namespace CabTap.Shared.User;

public class ClientDeleteViewModel
{
    public string Id { get; init; } = null!;
    
    [Display(Name = "Username")]
    public string UserName { get; init; } = null!;

    [Display(Name = "First Name")]
    public string FirstName { get; init; } = null!;

    [Display(Name = "Last Name")]
    public string LastName { get; init; } = null!;
    
    public string Email { get; init; } = null!;

    public string Address { get; init; } = null!;
}
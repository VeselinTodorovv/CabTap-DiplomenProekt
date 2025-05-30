namespace CabTap.Shared.User;

public class ClientIndexViewModel
{
    public string Id { get; init; } = null!;
    
    public string UserName { get; init; } = null!;

    public string FirstName { get; init; } = null!;

    public string LastName { get; init; } = null!;
    
    public string Email { get; init; } = null!;

    public string Address { get; init; } = null!;
    
    public bool IsAdmin { get; set; }
}
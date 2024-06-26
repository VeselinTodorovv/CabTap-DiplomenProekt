namespace CabTap.Shared.Driver;

public class DriverDetailsViewModel
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;
    
    public DateTime CreatedOn { get; set; }
    
    public DateTime LastModifiedOn { get; set; }
    
    public string CreatedBy { get; set; } = null!;
    
    public string LastModifiedBy { get; set; } = null!;
    
}
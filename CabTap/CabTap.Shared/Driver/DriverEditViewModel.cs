using System.ComponentModel.DataAnnotations;

namespace CabTap.Shared.Driver;

public class DriverEditViewModel
{
    public string Id { get; set; } = null!;

    [Required]
    public string Name { get; set; } = null!;
    
    public DateTime CreatedOn { get; set; }
    public DateTime LastModifiedOn { get; set; }
    public string CreatedBy { get; set; }
    public string LastModifiedBy { get; set; }
}
using System.ComponentModel.DataAnnotations;

namespace CabTap.Shared.Driver;

public class DriverCreateViewModel
{
    [Required]
    public string Name { get; set; } = null!;
}
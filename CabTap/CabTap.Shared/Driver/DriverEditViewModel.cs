using System.ComponentModel.DataAnnotations;

namespace CabTap.Shared.Driver;

public class DriverEditViewModel
{
    public string Id { get; set; } = null!;

    [Required]
    public string Name { get; set; } = null!;
}
using System.ComponentModel.DataAnnotations;

namespace CabTap.Shared.Driver;

public class DriverPairViewModel
{
    public string Id { get; set; } = null!;

    [Display(Name = "Driver Name")]
    public string Name { get; set; } = null!;
}
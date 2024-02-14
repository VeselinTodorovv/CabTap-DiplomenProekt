using System.ComponentModel.DataAnnotations;

namespace CabTap.Shared.Driver;

public class DriverPairViewModel
{
    public string Id { get; set; }

    [Display(Name = "Driver Name")]
    public string Name { get; set; } = null!;
}
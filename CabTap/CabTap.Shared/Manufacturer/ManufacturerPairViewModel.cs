using System.ComponentModel.DataAnnotations;

namespace CabTap.Shared.Manufacturer;

public class ManufacturerPairViewModel
{
    public int Id { get; set; }

    [Display(Name = "Manufacturer")]
    public string Name { get; set; } = null!;
}
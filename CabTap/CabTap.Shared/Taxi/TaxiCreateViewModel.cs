using System.ComponentModel.DataAnnotations;
using CabTap.Core.Entities.Enums;
using CabTap.Shared.Category;
using CabTap.Shared.Driver;
using CabTap.Shared.Manufacturer;

namespace CabTap.Shared.Taxi;

public class TaxiCreateViewModel
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    [RegularExpression(@"^[A-Z]{1,2}\d{4}[A-Z]{1,2}$", ErrorMessage = "Invalid registration number")]
    public string RegNumber { get; set; } = null!;
    
    [Required]
    [Display(Name = "Manufacturer")]
    public int ManufacturerId { get; set; }
    
    public virtual List<ManufacturerPairViewModel> Manufacturers { get; set; } = new();
    
    [Required]
    [Display(Name = "Category")]
    public int CategoryId { get; set; }

    public virtual List<CategoryPairViewModel> Categories { get; set; } = new();
    
    [Required]
    [Display(Name = "Driver")]
    public string DriverId { get; set; } = null!;

    public virtual List<DriverPairViewModel> Drivers { get; set; } = new();
    
    public string? Description { get; set; }
    
    [Url(ErrorMessage = "Please enter a valid URL")]
    [DataType(DataType.ImageUrl)]
    public string? Picture { get; set; }
    
    [Required]
    [Display(Name = "Status")]
    public TaxiStatus TaxiStatus { get; set; }
    
    [Required]
    [Range(1, 5, ErrorMessage = "Passenger Seats must be between 1 and 5")]
    public int PassengerSeats { get; set; }
}
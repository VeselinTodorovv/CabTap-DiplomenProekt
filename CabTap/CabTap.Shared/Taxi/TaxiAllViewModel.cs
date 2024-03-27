using System.ComponentModel.DataAnnotations;
using CabTap.Core.Entities.Enums;

namespace CabTap.Shared.Taxi;

public class TaxiAllViewModel
{
    public int Id { get; set; }
    public string RegNumber { get; set; } = null!;
    public int ManufacturerId { get; set; }
    
    [Display(Name = "Manufacturer")]
    public string ManufacturerName { get; set; } = null!;
    
    public int CategoryId { get; set; }
    
    [Display(Name = "Category")]
    public string CategoryName { get; set; } = null!;
    
    public string DriverId { get; set; } = null!;
    
    [Display(Name = "Driver")]
    public string DriverName { get; set; } = null!;
    public string? Description { get; set; }
    public string? Picture { get; set; }
    public TaxiStatus TaxiStatus { get; set; }
    public int PassengerSeats { get; set; }
}
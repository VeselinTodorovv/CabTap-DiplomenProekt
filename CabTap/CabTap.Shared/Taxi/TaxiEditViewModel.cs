using System.ComponentModel.DataAnnotations;
using CabTap.Core.Entities.Enums;

namespace CabTap.Shared.Taxi;

public class TaxiEditViewModel
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    public string RegNumber { get; set; } = null!;
    
    [Required]
    public int ManufacturerId { get; set; }
    
    [Required]
    public int CategoryId { get; set; }
    
    [Required]
    public string DriverId { get; set; } = null!;
    
    public string? Description { get; set; } = null!;
    
    [Url(ErrorMessage = "Please enter a valid URL")]
    [DataType(DataType.ImageUrl)]
    public string? Picture { get; set; }
    
    [Required]
    [Display(Name = "Status")]
    public TaxiStatus TaxiStatus { get; set; }
    
    [Required]
    [Range(1, 8, ErrorMessage = "Passenger Seats must be between 1 and 8")]
    public int PassengerSeats { get; set; }
    
    public DateTime CreatedOn { get; set; }
    public DateTime LastModifiedOn { get; set; }
    public string CreatedBy { get; set; } = null!;
    public string LastModifiedBy { get; set; } = null!;
}
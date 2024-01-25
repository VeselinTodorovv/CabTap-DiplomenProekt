using System.ComponentModel.DataAnnotations;
using CabTap.Core.Entities.Enums;

namespace CabTap.Shared.Taxi;

public class TaxiCreateViewModel
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    public string RegNumber { get; set; } = null!;
    
    [Required]
    public string Manufacturer { get; set; } = null!;
    
    [Required]
    public int CategoryId { get; set; }
    
    [Required]
    public string DriverId { get; set; } = null!;
    
    public string? Description { get; set; }
    
    [Url(ErrorMessage = "Please enter a valid URL")]
    [DataType(DataType.ImageUrl)]
    public string? Picture { get; set; }
    
    [Required]
    [Display(Name = "Status")]
    public TaxiStatus TaxiStatus { get; set; }
    
    [Required]
    [Range(1, 8, ErrorMessage = "Passenger Seats must be between 1 and 8")]
    public int PassengerSeats { get; set; }
    
    [Required]
    [DataType(DataType.DateTime)]
    [DisplayFormat(DataFormatString = "{dd/MM/yyyy hh:mm tt}")]
    public DateTime CreatedOn { get; set; }

    [Required]
    [DataType(DataType.DateTime)]
    [DisplayFormat(DataFormatString = "{dd/MM/yyyy hh:mm tt}")]
    public DateTime LastModifiedOn { get; set; }
    
    [Required]
    public string CreatedBy { get; set; } = null!;
    
    [Required]
    public string LastModifiedBy { get; set; } = null!;
}
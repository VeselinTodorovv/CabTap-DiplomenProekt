using System.ComponentModel.DataAnnotations;
using CabTap.Core.Entities.Enums;

namespace CabTap.Shared.Reservation;

public class ReservationDetailsViewModel
{
    [Key]
    public string Id { get; set; } = null!;

    [Required]
    public string UserId { get; set; } = null!;

    public int TaxiId { get; set; }
    
    [Required]
    public string Origin { get; set; } = null!;

    [Required]
    public string Destination { get; set; } = null!;

    [Required]
    public double Duration { get; set; }

    [Required]
    public decimal Price { get; set; }
    
    [Display(Name = "Passengers Count")]
    public int PassengersCount { get; set; }

    [Required]
    [EnumDataType(typeof(RideStatus))]
    public RideStatus RideStatus { get; set; }
    
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
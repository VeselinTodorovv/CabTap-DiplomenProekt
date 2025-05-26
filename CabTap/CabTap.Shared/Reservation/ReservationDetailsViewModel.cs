using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CabTap.Core.Entities.Enums;

namespace CabTap.Shared.Reservation;

public class ReservationDetailsViewModel
{
    [Key]
    public string Id { get; set; } = null!;

    public string UserId { get; set; } = null!;
    public Core.Entities.ApplicationUser User { get; set; } = null!;

    public int TaxiId { get; set; }
    public Core.Entities.Taxi Taxi { get; set; } = null!;
    
    [DataType(DataType.DateTime)]
    public DateTime ReservationDateTime { get; set; }
    
    public string Origin { get; set; } = null!;

    public string Destination { get; set; } = null!;
    
    [Required]
    public string OriginPoint { get; set; } = null!;
    
    [Required]
    public string DestinationPoint { get; set; } = null!;
    
    [Display(Name = "Reservation Type")]
    [EnumDataType(typeof(ReservationType))]
    public ReservationType ReservationType { get; set; }

    public double Duration { get; set; }
    
    public double Distance { get; set; }

    public decimal Price { get; set; }
    
    [Display(Name = "Passengers Count")]
    public int PassengersCount { get; set; }

    [Display(Name = "Ride Status")]
    [EnumDataType(typeof(RideStatus))]
    public RideStatus RideStatus { get; set; }
    
    [Display(Name = "Created On")]
    public DateTime CreatedOn { get; set; }
    
    [Display(Name = "Last Modified On")]
    public DateTime LastModifiedOn { get; set; }
    
    [Display(Name = "Created By")]
    public string CreatedBy { get; set; } = null!;
    
    [Display(Name = "Last Modified By")]
    public string LastModifiedBy { get; set; } = null!;
}
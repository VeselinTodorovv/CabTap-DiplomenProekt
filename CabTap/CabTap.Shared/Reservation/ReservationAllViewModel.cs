using System.ComponentModel.DataAnnotations;
using CabTap.Core.Entities;
using CabTap.Core.Entities.Enums;

namespace CabTap.Shared.Reservation;

public class ReservationAllViewModel
{
    public string Id { get; set; } = null!;

    public string UserId { get; set; } = null!;
    public ApplicationUser User { get; set; } = null!;

    public int TaxiId { get; set; }
    
    [Display(Name = "Time of Reservation")]
    [DataType(DataType.DateTime)]
    [DisplayFormat(DataFormatString = "{dd/MM/yyyy hh:mm}")]
    public DateTime ReservationDateTime { get; set; }
    
    [Display(Name = "From")]
    public string Origin { get; set; } = null!;

    [Display(Name = "To")]
    public string Destination { get; set; } = null!;
    
    [Display(Name = "Reservation Type")]
    [EnumDataType(typeof(ReservationType))]
    public ReservationType ReservationType { get; set; }

    public double Duration { get; set; }
    
    public double Distance { get; set; }

    public decimal Price { get; set; }
    
    [Display(Name = "Passengers")]
    public int PassengersCount { get; set; }

    [Display(Name = "Ride Status")]
    [EnumDataType(typeof(RideStatus))]
    public RideStatus RideStatus { get; set; }
}
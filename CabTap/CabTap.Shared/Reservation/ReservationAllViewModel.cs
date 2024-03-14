using System.ComponentModel.DataAnnotations;
using CabTap.Core.Entities.Enums;

namespace CabTap.Shared.Reservation;

public class ReservationAllViewModel
{
    public string Id { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public int TaxiId { get; set; }
    
    [DataType(DataType.DateTime)]
    [DisplayFormat(DataFormatString = "{dd/MM/yyyy hh:mm tt}")]
    public DateTime ReservationDateTime { get; set; }
    
    public string Origin { get; set; } = null!;

    public string Destination { get; set; } = null!;

    public double Duration { get; set; }
    
    public double Distance { get; set; }

    public decimal Price { get; set; }
    
    [Display(Name = "Passengers Count")]
    public int PassengersCount { get; set; }

    [EnumDataType(typeof(RideStatus))]
    public RideStatus RideStatus { get; set; }
    
    public DateTime CreatedOn { get; set; }
    
    public DateTime LastModifiedOn { get; set; }
    
    public string CreatedBy { get; set; } = null!;
    
    public string LastModifiedBy { get; set; } = null!;
}
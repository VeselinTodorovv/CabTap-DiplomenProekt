using System.ComponentModel.DataAnnotations;
using CabTap.Core.Entities;
using CabTap.Core.Entities.Enums;

namespace CabTap.Shared.Reservation;

public class ReservationDeleteViewModel
{
    public string Id { get; set; } = null!;

    public string UserId { get; set; } = null!;
    public ApplicationUser User { get; set; }

    public int TaxiId { get; set; }
    
    [DataType(DataType.DateTime)]
    [DisplayFormat(DataFormatString = "{dd/MM/yyyy hh:mm}")]
    public DateTime ReservationDateTime { get; set; }
    
    public string Origin { get; set; } = null!;

    public string Destination { get; set; } = null!;

    [Display(Name = "Reservation Type")]
    [EnumDataType(typeof(ReservationType))]
    public ReservationType ReservationType { get; set; }

    public double Duration { get; set; }
    
    public double Distance { get; set; }

    public decimal Price { get; set; }
    
    [Display(Name = "Passengers Count")]
    public int PassengersCount { get; set; }

    public RideStatus RideStatus { get; set; }
    
    [DataType(DataType.DateTime)]
    [DisplayFormat(DataFormatString = "{dd/MM/yyyy hh:mm}")]
    public DateTime CreatedOn { get; set; }
    
    [DataType(DataType.DateTime)]
    [DisplayFormat(DataFormatString = "{dd/MM/yyyy hh:mm}")]
    public DateTime LastModifiedOn { get; set; }
    
    public string CreatedBy { get; set; } = null!;
    
    public string LastModifiedBy { get; set; } = null!;
}
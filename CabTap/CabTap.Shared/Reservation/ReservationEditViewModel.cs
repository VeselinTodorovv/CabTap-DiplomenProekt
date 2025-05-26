using System.ComponentModel.DataAnnotations;
using CabTap.Core.Entities.Enums;
using CabTap.Shared.Category;

namespace CabTap.Shared.Reservation;

public class ReservationEditViewModel
{
    [Key]
    public string Id { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public int TaxiId { get; set; }

    [Display(Name = "Taxi Category")]
    public int CategoryId { get; set; }

    public virtual List<CategoryPairViewModel> Categories { get; set; } = new();
    
    [Required]
    [DataType(DataType.DateTime)]
    [DisplayFormat(DataFormatString = "{dd/MM/yyyy hh:mm}")]
    public DateTime ReservationDateTime { get; set; }

    [Required]
    public string Origin { get; set; } = null!;

    [Required]
    public string Destination { get; set; } = null!;
    
    [Required]
    public string OriginPoint { get; set; } = null!;
    
    [Required]
    public string DestinationPoint { get; set; } = null!;
    
    [Required]
    [EnumDataType(typeof(ReservationType))]
    [Display(Name = "Reservation Type")]
    public ReservationType ReservationType { get; set; }

    [Required]
    public double Duration { get; set; }
    
    public double Distance { get; set; }

    [Required]
    public decimal Price { get; set; }
    
    [Display(Name = "Passengers Count")]
    public int PassengersCount { get; set; }

    [Required]
    [EnumDataType(typeof(RideStatus))]
    public RideStatus RideStatus { get; set; }
}
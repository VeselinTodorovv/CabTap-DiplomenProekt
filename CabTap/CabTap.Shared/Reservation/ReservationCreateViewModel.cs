using System.ComponentModel.DataAnnotations;
using CabTap.Core.Entities.Enums;
using CabTap.Shared.Category;

namespace CabTap.Shared.Reservation;

public class ReservationCreateViewModel
{
    [Display(Name = "Taxi Category")]
    public int CategoryId { get; set; }

    public virtual List<CategoryPairViewModel> TaxiCategories { get; set; } = new();
    
    [Required]
    [DataType(DataType.DateTime)]
    [DisplayFormat(DataFormatString = "{dd/MM/yyyy hh:mm}")]
    public DateTime ReservationDateTime { get; set; }

    [Required]
    public string Origin { get; set; } = null!;

    [Required]
    public string Destination { get; set; } = null!;
    
    [Required]
    [EnumDataType(typeof(ReservationType))]
    public ReservationType ReservationType { get; set; }

    [Required]
    public double Duration { get; set; }
    
    [Required]
    public double Distance { get; set; }

    [Required]
    public decimal Price { get; set; }
    
    [Display(Name = "Passengers")]
    public int PassengersCount { get; set; }

    [Required]
    [EnumDataType(typeof(RideStatus))]
    // Set in progress by default
    public RideStatus RideStatus { get; set; } = RideStatus.InProgress;
}
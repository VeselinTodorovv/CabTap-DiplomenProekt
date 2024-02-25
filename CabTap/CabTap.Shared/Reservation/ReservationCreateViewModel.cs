using System.ComponentModel.DataAnnotations;
using CabTap.Core.Entities.Enums;
using CabTap.Shared.Category;

namespace CabTap.Shared.Reservation;

public class ReservationCreateViewModel
{
    [Required]
    public string UserId { get; set; } = null!;

    [Required]
    public int TaxiId { get; set; }
    
    [Display(Name = "Taxi Category")]
    public int CategoryId { get; set; }

    public virtual IEnumerable<CategoryPairViewModel> TaxiCategories { get; set; } = null!;

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
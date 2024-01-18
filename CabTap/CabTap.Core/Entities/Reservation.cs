using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CabTap.Core.Entities.Enums;

namespace CabTap.Core.Entities;

public class Reservation : BaseEntity
{
    [Key]
    public string Id { get; set; } = null!;

    [Required]
    public string UserId { get; set; } = null!;

    [ForeignKey(nameof(Taxi))]
    public int TaxiId { get; set; }
    public virtual Taxi Taxi { get; set; } = null!;
    
    [Required]
    //start location
    public string Origin { get; set; } = null!;

    [Required]
    public string Destination { get; set; } = null!;

    [Display(Name = "Passengers Count")]
    //TODO: Check if this value is less or equal to the taxi's max seats
    public int PassengersCount { get; set; }

    [Required]
    [EnumDataType(typeof(RideStatus))]
    public RideStatus RideStatus { get; set; }
}
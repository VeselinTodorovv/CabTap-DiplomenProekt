using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CabTap.Core.Entities.Enums;

namespace CabTap.Core.Entities;

public class Reservation
{
    [Key]
    public string Id { get; set; } = null!;

    [Required]
    // If using one database, make it a foreign key
    public string UserId { get; set; } = null!;

    [ForeignKey(nameof(Taxi))]
    public int TaxiId { get; set; }
    public Taxi Taxi { get; set; } = null!;

    [DataType(DataType.DateTime)]
    [DisplayFormat(DataFormatString = "{dd/MM/yyyy hh:mm tt}")]
    [Display(Name = "Time and Date of Reservation")]
    public DateTime ReservationTime { get; set; }

    [Required]
    //start location
    public string Origin { get; set; } = null!;

    [Required]
    public string Destination { get; set; } = null!;

    [Display(Name = "Passengers Count")]
    //TODO: Check if this value is less or equal to the taxi's max seats
    public int PassengersCount { get; set; }

    [Required]
    public RideStatus RideStatus { get; set; }
}
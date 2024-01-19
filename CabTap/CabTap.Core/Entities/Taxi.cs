using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CabTap.Core.Entities.Enums;

namespace CabTap.Core.Entities;

public class Taxi : BaseEntity
{
    [Key]
    public int Id { get; set; }

    //use regex to validate format
    [Required]
    public string RegNumber { get; set; } = null!;
    
    [Required]
    public string Brand { get; set; } = null!;

    [ForeignKey(nameof(Category))]
    public int CategoryId { get; set; }
    public Category Category { get; set; } = null!;

    [ForeignKey(nameof(Driver))]
    public string DriverId { get; set; } = null!;
    public Driver Driver { get; set; } = null!;

    public string? Description { get; set; } = null!;

    [Url(ErrorMessage = "Please enter a valid URL")]
    [DataType(DataType.ImageUrl)]
    public string? Picture { get; set; }

    [Display(Name = "Status")]
    [EnumDataType(typeof(TaxiStatus))]
    public TaxiStatus TaxiStatus { get; set; }

    [Range(1, 8, ErrorMessage = "Passenger Seats must be between 1 and 8")]
    public int PassengerSeats { get; set; }

    public virtual ICollection<Reservation> Reservations { get; set; }
}
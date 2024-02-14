using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CabTap.Core.Entities.Enums;

namespace CabTap.Core.Entities;

public class Taxi : BaseEntity
{
    [Key]
    public int Id { get; set; }

    //TODO: use regex to validate format
    [Required]
    public string RegNumber { get; set; } = null!;
    
    [Required]
    public string Manufacturer { get; set; } = null!;

    [ForeignKey(nameof(Category))]
    public int CategoryId { get; set; }
    public virtual Category Category { get; set; } = null!;

    [ForeignKey(nameof(Driver))]
    public string DriverId { get; set; } = null!;
    public virtual Driver Driver { get; set; } = null!;

    public string? Description { get; set; }

    [Url]
    [DataType(DataType.ImageUrl)]
    public string? Picture { get; set; }

    [EnumDataType(typeof(TaxiStatus))]
    public TaxiStatus TaxiStatus { get; set; }

    [Range(4, 8)]
    public int PassengerSeats { get; set; }

    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}
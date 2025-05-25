using System.ComponentModel.DataAnnotations;
using CabTap.Core.Entities.Enums;

namespace CabTap.Core.Entities;

public class Taxi : BaseEntity
{
    public int Id { get; set; }

    public string RegNumber { get; set; } = null!;
    
    public int ManufacturerId { get; set; }
    public virtual Manufacturer Manufacturer { get; set; } = null!; 

    public int CategoryId { get; set; }
    public virtual Category Category { get; set; } = null!;

    public string DriverId { get; set; } = null!;
    public virtual Driver Driver { get; set; } = null!;

    public string? Description { get; set; }

    [Url]
    [DataType(DataType.ImageUrl)]
    public string? Picture { get; set; }

    [EnumDataType(typeof(TaxiStatus))]
    public TaxiStatus TaxiStatus { get; set; }

    [Range(1, 5)]
    public int PassengerSeats { get; set; }

    public virtual IEnumerable<Reservation> Reservations { get; set; } = new List<Reservation>();
}
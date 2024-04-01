using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CabTap.Core.Entities.Enums;

namespace CabTap.Core.Entities;

public class Reservation : BaseEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; } = null!;

    [Required]
    [ForeignKey(nameof(User))]
    public string UserId { get; set; } = null!;
    public virtual ApplicationUser User { get; set; } = null!;

    [Required]
    [ForeignKey(nameof(Taxi))]
    public int TaxiId { get; set; }
    public virtual Taxi Taxi { get; set; } = null!;
    
    [Required]
    [DataType(DataType.DateTime)]
    public DateTime ReservationDateTime { get; set; } 
    
    [Required]
    //start location
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
    
    [Range(1, 5)]
    public int PassengersCount { get; set; }

    [Required]
    [EnumDataType(typeof(RideStatus))]
    public RideStatus RideStatus { get; set; }
}
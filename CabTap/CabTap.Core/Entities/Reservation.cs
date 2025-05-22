using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CabTap.Core.Entities.Enums;
using NetTopologySuite.Geometries;

namespace CabTap.Core.Entities;

public class Reservation : BaseEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; } = null!;

    [Required]
    [ForeignKey(nameof(User))]
    [StringLength(50)]
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
    [StringLength(250)]
    public string Origin { get; set; } = null!;

    [Required]
    [StringLength(250)]
    public string Destination { get; set; } = null!;
    
    [Column(TypeName = "geometry (Point, 4326)")]
    public Point Location { get; set; } = null!;
    
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
using System.ComponentModel.DataAnnotations;
using CabTap.Core.Entities.Enums;
using NetTopologySuite.Geometries;

namespace CabTap.Core.Entities;

public class Reservation : BaseEntity
{
    public string Id { get; set; } = null!;

    public string UserId { get; set; } = null!;
    public virtual ApplicationUser User { get; set; } = null!;

    public int TaxiId { get; set; }
    public virtual Taxi Taxi { get; set; } = null!;
    
    [DataType(DataType.DateTime)]
    public DateTime ReservationDateTime { get; set; } 
    
    public string Origin { get; set; } = null!;

    public string Destination { get; set; } = null!;
    
    public Point OriginPoint { get; set; } = null!;
    
    public Point DestinationPoint { get; set; } = null!;
    
    [EnumDataType(typeof(ReservationType))]
    public ReservationType ReservationType { get; set; }

    public double Duration { get; set; }
    
    public double Distance { get; set; }

    public decimal Price { get; set; }
    
    [Range(1, 5)]
    public int PassengersCount { get; set; }

    [EnumDataType(typeof(RideStatus))]
    public RideStatus RideStatus { get; set; }
}
using CabTap.Core.Entities.Enums;

namespace CabTap.Shared.Taxi;

public class TaxiDetailsViewModel
{
    public int Id { get; set; }
    public string RegNumber { get; set; } = null!;
    public string Brand { get; set; } = null!;
    public int CategoryId { get; set; }
    public string DriverId { get; set; } = null!;
    public string? Description { get; set; } = null!;
    public string? Picture { get; set; }
    public TaxiStatus TaxiStatus { get; set; }
    public int PassengerSeats { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime LastModifiedOn { get; set; }
    public string CreatedBy { get; set; } = null!;
    public string LastModifiedBy { get; set; } = null!;
}
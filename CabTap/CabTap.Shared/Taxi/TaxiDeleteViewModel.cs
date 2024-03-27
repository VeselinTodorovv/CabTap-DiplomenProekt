using CabTap.Core.Entities.Enums;

namespace CabTap.Shared.Taxi;

public class TaxiDeleteViewModel
{
    public int Id { get; set; }
    public string RegNumber { get; set; } = null!;
    public int ManufacturerId { get; set; }
    public int CategoryId { get; set; }
    public string DriverId { get; set; } = null!;
    public string? Description { get; set; }
    public string? Picture { get; set; }
    public TaxiStatus TaxiStatus { get; set; }
    public int PassengerSeats { get; set; }
}
using CabTap.Core.Entities.Enums;

namespace CabTap.Shared.Taxi;

public class TaxiAllViewModel
{
    public int Id { get; set; }

    public string RegNumber { get; set; } = null!;
    
    public string Brand { get; set; } = null!;

    public int CategoryId { get; set; }

    public string DriverId { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string? Picture { get; set; }

    public TaxiStatus TaxiStatus { get; set; }

    public int PassengerSeats { get; set; }
}
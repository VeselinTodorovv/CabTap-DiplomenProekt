namespace CabTap.Contracts.Services;

public interface IStatisticService
{
    // TODO: Make this more useful, not just a wrapper
    Task<int> CountTaxisAsync();
    Task<int> CountDriversAsync();
    Task<int> CountClientsAsync();
    Task<int> CountReservationsAsync();
    Task<int> CountReservationsAsync(string userId);
    Task<decimal> SumReservationsAsync();
}
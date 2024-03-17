namespace CabTap.Contracts.Services;

public interface IStatisticService
{
    Task<int> CountTaxisAsync();
    Task<int> CountDriversAsync();
    Task<int> CountClientsAsync();
    Task<int> CountReservationsAsync();
    Task<int> CountReservationsAsync(string userId);
    Task<decimal> SumReservationsAsync();
}
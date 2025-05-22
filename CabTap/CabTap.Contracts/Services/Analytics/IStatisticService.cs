namespace CabTap.Contracts.Services.Analytics;

public interface IStatisticService
{
    Task<int> CountTaxisAsync();
    Task<int> CountDriversAsync();
    Task<int> CountClientsAsync();
    Task<int> CountReservationsAsync();
    Task<int> CountReservationsAsync(string userName);
    Task<decimal> SumReservationsAsync();
}
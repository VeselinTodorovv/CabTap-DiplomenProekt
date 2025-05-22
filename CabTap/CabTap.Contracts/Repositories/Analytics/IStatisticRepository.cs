namespace CabTap.Contracts.Repositories.Analytics;

public interface IStatisticRepository
{
    Task<int> CountTaxisAsync();
    Task<int> CountDriversAsync();
    Task<int> CountClientsAsync();
    Task<int> CountReservationsAsync();
    Task<int> CountReservationsAsync(string userName);
    Task<decimal> SumReservationsAsync();
}
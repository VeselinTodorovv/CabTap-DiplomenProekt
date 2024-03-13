namespace CabTap.Contracts.Repositories;

public interface IStatisticRepository
{
    Task<int> CountTaxisAsync();
    Task<int> CountDriversAsync();
    Task<int> CountClientsAsync();
    Task<int> CountReservationsAsync();
    Task<int> CountReservationsAsync(string userId);
    Task<decimal> SumReservationsAsync();
}
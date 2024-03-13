using CabTap.Core.Entities;

namespace CabTap.Contracts.Repositories;

public interface ITaxiRepository
{
    Task<IEnumerable<Taxi>> GetAllTaxisAsync();
    Task<IEnumerable<Taxi>> GetPaginatedTaxisAsync(int page, int pageSize);
    Task<Taxi> GetTaxiByIdAsync(int taxiId);
    Task AddTaxiAsync(Taxi taxi);
    Task UpdateTaxiAsync(Taxi taxi);
    Task DeleteTaxiAsync(int taxiId);
}
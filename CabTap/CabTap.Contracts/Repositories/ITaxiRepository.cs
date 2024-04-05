using CabTap.Core.Entities;

namespace CabTap.Contracts.Repositories;

public interface ITaxiRepository
{
    IQueryable<Taxi> GetTaxisQuery();
    Task<Taxi> GetTaxiByIdAsync(int taxiId);
    Task AddTaxiAsync(Taxi taxi);
    Task UpdateTaxiAsync(Taxi taxi);
    Task DeleteTaxiAsync(int taxiId);
}
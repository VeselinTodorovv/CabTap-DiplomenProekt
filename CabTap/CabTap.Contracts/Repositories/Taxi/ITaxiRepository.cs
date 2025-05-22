namespace CabTap.Contracts.Repositories.Taxi;

public interface ITaxiRepository
{
    IQueryable<Core.Entities.Taxi> GetTaxisQuery();
    Task<Core.Entities.Taxi> GetTaxiByIdAsync(int taxiId);
    Task AddTaxiAsync(Core.Entities.Taxi taxi);
    Task UpdateTaxiAsync(Core.Entities.Taxi taxi);
    Task DeleteTaxiAsync(int taxiId);
}
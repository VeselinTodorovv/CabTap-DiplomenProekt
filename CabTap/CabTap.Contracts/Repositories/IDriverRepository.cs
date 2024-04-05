using CabTap.Core.Entities;

namespace CabTap.Contracts.Repositories;

public interface IDriverRepository
{
    IQueryable<Driver> GetDriversQuery();
    Task<Driver> GetDriverByIdAsync(string driverId);
    Task AddDriverAsync(Driver driver);
    Task UpdateDriverAsync(Driver driver);
    Task DeleteDriverAsync(string driverId);
}
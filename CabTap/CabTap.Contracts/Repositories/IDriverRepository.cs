using CabTap.Core.Entities;

namespace CabTap.Contracts.Repositories;

public interface IDriverRepository
{
    Task<IEnumerable<Driver?>> GetAllDriversAsync();
    Task<Driver> GetDriverByIdAsync(int driverId);
    Task AddDriverAsync(Driver driver);
    Task UpdateDriverAsync(Driver driver);
    Task DeleteDriverAsync(int driverId);
}
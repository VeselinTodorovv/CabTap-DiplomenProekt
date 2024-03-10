using CabTap.Core.Entities;

namespace CabTap.Contracts.Repositories;

public interface IDriverRepository
{
    Task<IEnumerable<Driver>> GetAllDriversAsync();
    Task<Driver> GetDriverByIdAsync(string driverId);
    Task AddDriverAsync(Driver driver);
    Task UpdateDriverAsync(Driver driver);
    Task DeleteDriverAsync(string driverId);
    Task<IEnumerable<Driver>> GetPaginatedDriversAsync(int page, int pageSize);
}
using CabTap.Shared.Driver;

namespace CabTap.Contracts.Services;

public interface IDriverService
{
    IEnumerable<DriverAllViewModel> GetAllDriversAsync();
    Task<IEnumerable<DriverAllViewModel>> GetPaginatedDriversAsync(int page, int pageSize);
    Task<DriverDetailsViewModel> GetDriverByIdAsync(string driverId);
    Task AddDriverAsync(DriverCreateViewModel driverViewModel);
    Task UpdateDriverAsync(DriverEditViewModel driverViewModel);
    Task DeleteDriverAsync(string driverId);
}
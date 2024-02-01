using CabTap.Shared.Driver;

namespace CabTap.Contracts.Services;

public interface IDriverService
{
    Task<IEnumerable<DriverAllViewModel>> GetAllDriversAsync();
    Task<DriverDetailsViewModel> GetDriverByIdAsync(int driverId);
    Task AddDriverAsync(DriverCreateViewModel driverViewModel);
    Task UpdateDriverAsync(DriverEditViewModel driverViewModel);
    Task DeleteDriverAsync(string driverId);
}
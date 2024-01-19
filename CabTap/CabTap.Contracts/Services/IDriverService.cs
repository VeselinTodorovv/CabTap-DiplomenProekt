using CabTap.Shared.Driver;

namespace CabTap.Contracts.Services;

public interface IDriverService
{
    Task<IEnumerable<DriverAllViewModel>> GetAllDriversAsync();
    Task<DriverDetailsViewModel> GetDriverByIdAsync(int taxiId);
    Task AddDriverAsync(DriverCreateViewModel taxiViewModel);
    Task UpdateDriverAsync(DriverEditViewModel taxiViewModel);
    Task DeleteDriverAsync(int taxiId);
}
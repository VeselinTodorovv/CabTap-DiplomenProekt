using CabTap.Contracts.Repositories;
using CabTap.Contracts.Services;
using CabTap.Core.Entities;
using CabTap.Shared.Driver;

namespace CabTap.Services.Services;

public class DriverService : IDriverService
{
    private readonly IDriverRepository _driverRepository;
    private readonly IUserService _userService;
    
    public DriverService(IDriverRepository driverRepository, IUserService userService)
    {
        _driverRepository = driverRepository;
        _userService = userService;
    }
    
    public async Task<IEnumerable<DriverAllViewModel>> GetAllDriversAsync()
    {
        var drivers = await _driverRepository.GetAllDriversAsync();

        var driverViewModels = drivers.Select(driver => new DriverAllViewModel
        {
            Id = driver!.Id,
            Name = driver.Name,
            CreatedBy = driver.CreatedBy,
            CreatedOn = driver.CreatedOn,
            LastModifiedBy = driver.LastModifiedBy,
            LastModifiedOn = driver.LastModifiedOn
        });

        return driverViewModels;
    }

    public async Task<DriverDetailsViewModel> GetDriverByIdAsync(string driverId)
    {
        var driver = await _driverRepository.GetDriverByIdAsync(driverId);

        var model = new DriverDetailsViewModel
        {
            Id = driver.Id,
            Name = driver.Name,
            CreatedBy = driver.CreatedBy,
            CreatedOn = driver.CreatedOn,
            LastModifiedBy = driver.LastModifiedBy,
            LastModifiedOn = driver.LastModifiedOn
        };

        return model;
    }

    public async Task AddDriverAsync(DriverCreateViewModel driverViewModel)
    {
        var user = await _userService.GetCurrentUserName();

        if (user != null)
        {
            var driver = new Driver
            {
                Name = driverViewModel.Name,
                
                CreatedBy = user,
                CreatedOn = DateTime.Now,
                LastModifiedBy = user,
                LastModifiedOn = DateTime.Now
            };
        
            await _driverRepository.AddDriverAsync(driver);
        }
    }

    public async Task UpdateDriverAsync(DriverEditViewModel driverViewModel)
    {
        var user = await _userService.GetCurrentUserName();

        if (user != null)
        {
            var driver = new Driver
            {
                Id = driverViewModel.Id,
                Name = driverViewModel.Name,
            
                CreatedBy = driverViewModel.CreatedBy,
                CreatedOn = driverViewModel.CreatedOn,
                LastModifiedBy = user,
                LastModifiedOn = DateTime.Now
            };
            
            await _driverRepository.UpdateDriverAsync(driver);
        }
    }

    public async Task DeleteDriverAsync(string driverId)
    {
        await _driverRepository.DeleteDriverAsync(driverId);
    }
}
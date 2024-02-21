using AutoMapper;
using CabTap.Contracts.Repositories;
using CabTap.Contracts.Services;
using CabTap.Core.Entities;
using CabTap.Shared.Driver;

namespace CabTap.Services.Services;

public class DriverService : IDriverService
{
    private readonly IDriverRepository _driverRepository;
    private readonly IUserService _userService;
    private readonly IMapper _mapper;
    
    public DriverService(IDriverRepository driverRepository, IUserService userService, IMapper mapper)
    {
        _driverRepository = driverRepository;
        _userService = userService;
        _mapper = mapper;
    }
    
    public async Task<IEnumerable<DriverAllViewModel>> GetAllDriversAsync()
    {
        var drivers = await _driverRepository.GetAllDriversAsync();

        var driverViewModels = _mapper.Map<IEnumerable<DriverAllViewModel>>(drivers);

        return driverViewModels;
    }

    public async Task<DriverDetailsViewModel> GetDriverByIdAsync(string driverId)
    {
        var driver = await _driverRepository.GetDriverByIdAsync(driverId);

        var model = _mapper.Map<DriverDetailsViewModel>(driver);

        return model;
    }

    public async Task AddDriverAsync(DriverCreateViewModel driverViewModel)
    {
        var user = await _userService.GetCurrentUserName();

        if (user != null)
        {
            var driver = _mapper.Map<Driver>(driverViewModel);

            driver.CreatedBy = user;
            driver.CreatedOn = DateTime.Now;
            driver.LastModifiedBy = user;
            driver.LastModifiedOn = DateTime.Now;
        
            await _driverRepository.AddDriverAsync(driver);
        }
    }

    public async Task UpdateDriverAsync(DriverEditViewModel driverViewModel)
    {
        var user = await _userService.GetCurrentUserName();

        if (user != null)
        {
            var driver = _mapper.Map<Driver>(driverViewModel);
            
            driver.LastModifiedBy = user;
            driver.LastModifiedOn = DateTime.Now;
            
            await _driverRepository.UpdateDriverAsync(driver);
        }
    }

    public async Task DeleteDriverAsync(string driverId)
    {
        await _driverRepository.DeleteDriverAsync(driverId);
    }
}
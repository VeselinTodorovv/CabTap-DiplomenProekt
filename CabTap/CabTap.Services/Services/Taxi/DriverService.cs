using AutoMapper;
using CabTap.Contracts.Repositories.Taxi;
using CabTap.Contracts.Services.Identity;
using CabTap.Contracts.Services.Taxi;
using CabTap.Contracts.Services.Utilities;
using CabTap.Core.Entities;
using CabTap.Services.Infrastructure;
using CabTap.Shared.Driver;

namespace CabTap.Services.Services.Taxi;

public class DriverService : IDriverService
{
    private readonly IDriverRepository _driverRepository;
    private readonly IUserService _userService;
    private readonly IDateTimeService _dateTimeService;
    private readonly IMapper _mapper;
    private readonly IAuditService _auditService;
    
    public DriverService(IDriverRepository driverRepository, IUserService userService, IMapper mapper, IDateTimeService dateTimeService, IAuditService auditService)
    {
        _driverRepository = driverRepository;
        _userService = userService;
        _dateTimeService = dateTimeService;
        _auditService = auditService;
        _mapper = mapper;
    }
    
    public IEnumerable<DriverAllViewModel> GetAllDriversAsync()
    {
        var drivers = _driverRepository.GetDriversQuery()
            .AsEnumerable();

        var driverViewModels = _mapper.Map<IEnumerable<DriverAllViewModel>>(drivers);

        return driverViewModels;
    }
    
    public async Task<IEnumerable<DriverAllViewModel>> GetPaginatedDriversAsync(int page, int pageSize)
    {
        var query = _driverRepository.GetDriversQuery();
        var drivers = await query.PaginateAsync(page, pageSize);
        
        var reservationViewModels = _mapper.Map<IEnumerable<DriverAllViewModel>>(drivers);
        return reservationViewModels;
    }

    public async Task<DriverDetailsViewModel> GetDriverByIdAsync(string driverId)
    {
        var driver = await _driverRepository.GetDriverByIdAsync(driverId);

        var model = _mapper.Map<DriverDetailsViewModel>(driver);

        return model;
    }

    public async Task AddDriverAsync(DriverCreateViewModel driverViewModel)
    {
        var user = await _userService.GetCurrentUserAsync();
        if (user == null)
        {
            throw new UnauthorizedAccessException("User is not logged in");
        }

        var driver = _mapper.Map<Driver>(driverViewModel);

        var dateTime = _dateTimeService.GetCurrentDateTime();
        _auditService.SetCreationAuditInfo(driver, dateTime, user.UserName);

        await _driverRepository.AddDriverAsync(driver);
    }

    public async Task UpdateDriverAsync(DriverEditViewModel driverViewModel)
    {
        var user = await _userService.GetCurrentUserAsync();
        if (user == null)
        {
            throw new InvalidOperationException();
        }

        var existingDriver = await _driverRepository.GetDriverByIdAsync(driverViewModel.Id);

        _mapper.Map(driverViewModel, existingDriver);

        var dateTime = _dateTimeService.GetCurrentDateTime();
        _auditService.SetModificationAuditInfo(existingDriver, dateTime, user.UserName);

        await _driverRepository.UpdateDriverAsync(existingDriver);
    }

    public async Task DeleteDriverAsync(string driverId)
    {
        await _driverRepository.DeleteDriverAsync(driverId);
    }
}
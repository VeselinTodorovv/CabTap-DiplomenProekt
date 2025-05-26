using AutoMapper;
using CabTap.Contracts.Repositories.Taxi;
using CabTap.Contracts.Services.Identity;
using CabTap.Contracts.Services.Taxi;
using CabTap.Contracts.Services.Utilities;
using CabTap.Services.Infrastructure;
using CabTap.Shared.Taxi;

namespace CabTap.Services.Services.Taxi;

public class TaxiService : ITaxiService
{
    private readonly ITaxiRepository _taxiRepository;
    private readonly IUserService _userService;
    private readonly IMapper _mapper;
    private readonly IAuditService _auditService;
    
    public TaxiService(ITaxiRepository taxiRepository, IUserService userService, IMapper mapper, ICategoryService categoryService, IAuditService auditService)
    {
        _taxiRepository = taxiRepository;
        _userService = userService;
        _auditService = auditService;
        _mapper = mapper;
    }

    public async Task<IEnumerable<TaxiAllViewModel>> GetPaginatedTaxisAsync(int page, int pageSize)
    {
        var query = _taxiRepository.GetTaxisQuery();
        var taxis = await query.PaginateAsync(page, pageSize);

        var reservationViewModels = _mapper.Map<IEnumerable<TaxiAllViewModel>>(taxis);
        return reservationViewModels;
    }

    public async Task<TaxiDetailsViewModel> GetTaxiByIdAsync(int taxiId)
    {
        var taxi = await _taxiRepository.GetTaxiByIdAsync(taxiId);

        var model = _mapper.Map<TaxiDetailsViewModel>(taxi);

        return model;
    }

    public async Task AddTaxiAsync(TaxiCreateViewModel taxiViewModel)
    {
        var user = await _userService.GetCurrentUserAsync();

        var taxi = _mapper.Map<Core.Entities.Taxi>(taxiViewModel);

        _auditService.SetCreationAuditInfo(taxi, user.UserName);

        await _taxiRepository.AddTaxiAsync(taxi);
    }

    public async Task UpdateTaxiAsync(TaxiEditViewModel taxiViewModel)
    {
        var user = await _userService.GetCurrentUserAsync();

        var existingTaxi = await _taxiRepository.GetTaxiByIdAsync(taxiViewModel.Id);

        _mapper.Map(taxiViewModel, existingTaxi);

        _auditService.SetModificationAuditInfo(existingTaxi, user.UserName);

        await _taxiRepository.UpdateTaxiAsync(existingTaxi);
    }

    public async Task DeleteTaxiAsync(int taxiId)
    {
        await _taxiRepository.DeleteTaxiAsync(taxiId);
    }
}
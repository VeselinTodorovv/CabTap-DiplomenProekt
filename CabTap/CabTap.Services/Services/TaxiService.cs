using CabTap.Contracts.Repositories;
using CabTap.Contracts.Services;
using CabTap.Core.Entities;
using CabTap.Core.Entities.Enums;
using CabTap.Shared.Taxi;

namespace CabTap.Services.Services;

public class TaxiService : ITaxiService
{
    private readonly ITaxiRepository _taxiRepository;
    
    public TaxiService(ITaxiRepository taxiRepository)
    {
        _taxiRepository = taxiRepository;
    }

    public async Task<IEnumerable<TaxiAllViewModel>> GetAllAvailableTaxisAsync()
    {
        var taxis = await _taxiRepository.GetAllTaxisAsync();

        var taxiViewModels = taxis.Select(taxi => new TaxiAllViewModel
        {
            Id = taxi!.Id,
            Brand = taxi.Brand,
            RegNumber = taxi.RegNumber,
            Description = taxi.Description,
            Picture = taxi.Picture,
            TaxiStatus = taxi.TaxiStatus,
            PassengerSeats = taxi.PassengerSeats,
            DriverId = taxi.DriverId,
            CategoryId = taxi.CategoryId
        })
            .Where(x => x.TaxiStatus == TaxiStatus.Available)
            .ToList();
        
        return taxiViewModels;
    }

    public async Task<TaxiDetailsViewModel> GetTaxiByIdAsync(int taxiId)
    {
        var taxi = await _taxiRepository.GetTaxiByIdAsync(taxiId);
        
        var model = new TaxiDetailsViewModel
        {
            // bind later
        };

        return model;
    }

    public async Task AddTaxiAsync(TaxiCreateViewModel taxiViewModel)
    {
        var taxi = new Taxi
        {
            //bind view model
        };
        
        await _taxiRepository.AddTaxiAsync(taxi);
    }

    public async Task UpdateTaxiAsync(TaxiEditViewModel taxiViewModel)
    {
        var taxi = new Taxi
        {
            //bind view model
        };
        
        await _taxiRepository.UpdateTaxiAsync(taxi);
    }

    public async Task DeleteTaxiAsync(int taxiId)
    {
        await _taxiRepository.DeleteTaxiAsync(taxiId);
    }
}
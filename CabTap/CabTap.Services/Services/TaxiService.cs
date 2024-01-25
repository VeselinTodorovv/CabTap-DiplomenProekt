using CabTap.Contracts.Repositories;
using CabTap.Contracts.Services;
using CabTap.Core.Entities;
using CabTap.Core.Entities.Enums;
using CabTap.Shared.Taxi;
using Microsoft.AspNetCore.Http;

namespace CabTap.Services.Services;

public class TaxiService : ITaxiService
{
    private readonly ITaxiRepository _taxiRepository;
    private readonly IHttpContextAccessor _contextAccessor;
    
    public TaxiService(ITaxiRepository taxiRepository, IHttpContextAccessor contextAccessor)
    {
        _taxiRepository = taxiRepository;
        _contextAccessor = contextAccessor;
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
            CategoryId = taxi.CategoryId,
            CreatedBy = taxi.CreatedBy,
            CreatedOn = taxi.CreatedOn,
            LastModifiedBy = taxi.LastModifiedBy,
            LastModifiedOn = taxi.LastModifiedOn
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
            Id = taxi.Id,
            Brand = taxi.Brand,
            Description = taxi.Description,
            Picture = taxi.Picture,
            CategoryId = taxi.CategoryId,
            CreatedBy = taxi.CreatedBy,
            CreatedOn = taxi.CreatedOn,
            DriverId = taxi.DriverId,
            PassengerSeats = taxi.PassengerSeats,
            RegNumber = taxi.RegNumber,
            TaxiStatus = taxi.TaxiStatus,
            LastModifiedBy = taxi.LastModifiedBy,
            LastModifiedOn = taxi.LastModifiedOn
        };

        return model;
    }

    public async Task AddTaxiAsync(TaxiCreateViewModel taxiViewModel)
    {
        var user = _contextAccessor.HttpContext.User.Identity?.Name;

        if (user != null)
        {
            var taxi = new Taxi
            {
                Id = taxiViewModel.Id,
                Brand = taxiViewModel.Brand,
                Description = taxiViewModel.Description,
                Picture = taxiViewModel.Picture,
                CategoryId = taxiViewModel.CategoryId,
                CreatedBy = user,
                CreatedOn = DateTime.Now,
                DriverId = taxiViewModel.DriverId,
                PassengerSeats = taxiViewModel.PassengerSeats,
                RegNumber = taxiViewModel.RegNumber,
                TaxiStatus = taxiViewModel.TaxiStatus,
                LastModifiedBy = user,
                LastModifiedOn = DateTime.Now
            };
        
            await _taxiRepository.AddTaxiAsync(taxi);
        }
    }

    public async Task UpdateTaxiAsync(TaxiEditViewModel taxiViewModel)
    {
        var user = _contextAccessor.HttpContext.User.Identity?.Name;

        if (user != null)
        {
            var taxi = new Taxi
            {
                Id = taxiViewModel.Id,
                Brand = taxiViewModel.Brand,
                Description = taxiViewModel.Description,
                Picture = taxiViewModel.Picture,
                CategoryId = taxiViewModel.CategoryId,
                
                // Maybe don't let the user edit these 2 values in the view?
                CreatedBy = taxiViewModel.CreatedBy,
                CreatedOn = taxiViewModel.CreatedOn,
                
                DriverId = taxiViewModel.DriverId,
                PassengerSeats = taxiViewModel.PassengerSeats,
                RegNumber = taxiViewModel.RegNumber,
                TaxiStatus = taxiViewModel.TaxiStatus,
                LastModifiedBy = user,
                LastModifiedOn = DateTime.Now
            };
        
            await _taxiRepository.UpdateTaxiAsync(taxi);
        }
    }

    public async Task DeleteTaxiAsync(int taxiId)
    {
        await _taxiRepository.DeleteTaxiAsync(taxiId);
    }
}
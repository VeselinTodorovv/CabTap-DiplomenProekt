using CabTap.Contracts.Repositories;
using CabTap.Contracts.Services;
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
        throw new NotImplementedException();
    }

    public async Task<TaxiDetailsViewModel> GetTaxiByIdAsync(int taxiId)
    {
        throw new NotImplementedException();
    }

    public async Task AddTaxiAsync(TaxiCreateViewModel taxiViewModel)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateTaxiAsync(TaxiEditViewModel taxiViewModel)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteTaxiAsync(int taxiId)
    {
        throw new NotImplementedException();
    }
}
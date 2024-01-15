using CabTap.Contracts.Repositories;
using CabTap.Contracts.Services;

namespace CabTap.Services.Services;

public class TaxiService : ITaxiService
{
    private readonly ITaxiRepository _taxiRepository;
    
    public TaxiService(ITaxiRepository taxiRepository)
    {
        _taxiRepository = taxiRepository;
    }
}
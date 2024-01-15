using CabTap.Contracts.Repositories;

namespace CabTap.Data.Repositories;

public class TaxiRepository : ITaxiRepository
{
    private readonly ApplicationDbContext _context;
    
    public TaxiRepository(ApplicationDbContext context)
    {
        _context = context;
    }
}
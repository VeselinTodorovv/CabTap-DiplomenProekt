using CabTap.Contracts.Repositories.Taxi;
using CabTap.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CabTap.Data.Repositories;

public class ManufacturerRepository : IManufacturerRepository
{
    private readonly ApplicationDbContext _context;

    public ManufacturerRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Manufacturer>> GetAllManufacturersAsync()
    {
        return await _context.Manufacturers.ToListAsync();
    }
}
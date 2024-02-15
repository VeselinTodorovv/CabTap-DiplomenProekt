using CabTap.Contracts.Repositories;
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

    public async Task<IEnumerable<Manufacturer>> GetAllManufacturers()
    {
        return await _context.Manufacturers.ToListAsync();
    }

    public async Task<Manufacturer> GetManufacturerById(int id)
    {
        return await _context.Manufacturers.FindAsync(id);
    }

    public async Task<IEnumerable<Taxi>> GetTaxisByManufacturer(int manufacturerId)
    {
        return await _context.Taxis
            .Where(x => x.ManufacturerId == manufacturerId)
            .ToListAsync();
    }
}
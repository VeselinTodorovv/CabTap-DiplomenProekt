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
        var manufacturer = await _context.Manufacturers.FindAsync(id);
        if (manufacturer == null)
        {
            throw new InvalidOperationException($"Manufacturer with id {id} not found.");
        }
        
        return manufacturer;
    }

    public async Task<IEnumerable<Taxi>> GetTaxisByManufacturer(int manufacturerId)
    {
        return await _context.Taxis
            .Where(x => x.ManufacturerId == manufacturerId)
            .ToListAsync();
    }
}
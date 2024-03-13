using CabTap.Contracts.Repositories;
using CabTap.Contracts.Services;
using CabTap.Shared.Manufacturer;

namespace CabTap.Services.Services;

public class ManufacturerService : IManufacturerService
{
    private readonly IManufacturerRepository _manufacturerRepository;
    
    public ManufacturerService(IManufacturerRepository manufacturerRepository)
    {
        _manufacturerRepository = manufacturerRepository;
    }

    public async Task<IEnumerable<ManufacturerPairViewModel>> GetAllManufacturersAsync()
    {
        var manufacturers = await _manufacturerRepository.GetAllManufacturersAsync();

        var model = manufacturers.Select(x => new ManufacturerPairViewModel
        {
            Id = x.Id,
            Name = x.Name
        });

        return model;
    }
}
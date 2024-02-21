using AutoMapper;
using CabTap.Contracts.Repositories;
using CabTap.Contracts.Services;
using CabTap.Shared.Manufacturer;
using CabTap.Shared.Taxi;

namespace CabTap.Services.Services;

public class ManufacturerService : IManufacturerService
{
    private readonly IManufacturerRepository _manufacturerRepository;
    private readonly IMapper _mapper;
    
    public ManufacturerService(IManufacturerRepository manufacturerRepository, IMapper mapper)
    {
        _manufacturerRepository = manufacturerRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ManufacturerPairViewModel>> GetAllManufacturers()
    {
        var manufacturers = await _manufacturerRepository.GetAllManufacturers();

        var model = manufacturers.Select(x => new ManufacturerPairViewModel
        {
            Id = x.Id,
            Name = x.Name
        });

        return model;
    }

    public async Task<ManufacturerPairViewModel> GetmanufacturerById(int id)
    {
        var manufacturer = await _manufacturerRepository.GetManufacturerById(id);

        var model = new ManufacturerPairViewModel
        {
            Id = manufacturer.Id,
            Name = manufacturer.Name
        };

        return model;
    }

    public async Task<IEnumerable<TaxiAllViewModel>> GetTaxisByManufacturer(int manufacturerId)
    {
        var taxisByManufacturer = await _manufacturerRepository.GetTaxisByManufacturer(manufacturerId);

        var model = _mapper.Map<IEnumerable<TaxiAllViewModel>>(taxisByManufacturer);

        return model;
    }
}
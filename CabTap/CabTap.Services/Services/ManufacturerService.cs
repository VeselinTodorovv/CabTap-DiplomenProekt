using CabTap.Contracts.Repositories;
using CabTap.Contracts.Services;
using CabTap.Shared.Manufacturer;
using CabTap.Shared.Taxi;

namespace CabTap.Services.Services;

public class ManufacturerService : IManufacturerService
{
    private readonly IManufacturerRepository _manufacturerRepository;
    
    public ManufacturerService(IManufacturerRepository manufacturerRepository)
    {
        _manufacturerRepository = manufacturerRepository;
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

        var model = taxisByManufacturer.Select(x => new TaxiAllViewModel
        {
            Id = x.Id,
            ManufacturerId = x.ManufacturerId,
            RegNumber = x.RegNumber,
            Description = x.Description,
            Picture = x.Picture,
            TaxiStatus = x.TaxiStatus,
            PassengerSeats = x.PassengerSeats,
            DriverId = x.DriverId,
            CategoryId = x.CategoryId,

            CreatedBy = x.CreatedBy,
            CreatedOn = x.CreatedOn,
            LastModifiedBy = x.LastModifiedBy,
            LastModifiedOn = x.LastModifiedOn,
        });

        return model;
    }
}
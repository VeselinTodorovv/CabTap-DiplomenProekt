using CabTap.Shared.Manufacturer;
using CabTap.Shared.Taxi;

namespace CabTap.Contracts.Services;

public interface IManufacturerService
{
    Task<IEnumerable<ManufacturerPairViewModel>> GetAllManufacturers();
    Task<ManufacturerPairViewModel> GetmanufacturerById(int id);
    Task<IEnumerable<TaxiAllViewModel>> GetTaxisByManufacturer(int manufacturerId);
}
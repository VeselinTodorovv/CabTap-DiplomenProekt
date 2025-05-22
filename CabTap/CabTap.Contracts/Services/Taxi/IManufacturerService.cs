using CabTap.Shared.Manufacturer;

namespace CabTap.Contracts.Services.Taxi;

public interface IManufacturerService
{
    Task<IEnumerable<ManufacturerPairViewModel>> GetAllManufacturersAsync();
}
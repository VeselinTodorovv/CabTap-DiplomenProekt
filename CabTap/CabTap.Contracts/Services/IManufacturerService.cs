using CabTap.Shared.Manufacturer;

namespace CabTap.Contracts.Services;

public interface IManufacturerService
{
    Task<IEnumerable<ManufacturerPairViewModel>> GetAllManufacturers();
}
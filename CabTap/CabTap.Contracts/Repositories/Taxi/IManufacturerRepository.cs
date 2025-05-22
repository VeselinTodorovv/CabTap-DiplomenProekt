using CabTap.Core.Entities;

namespace CabTap.Contracts.Repositories.Taxi;

public interface IManufacturerRepository
{
    Task<IEnumerable<Manufacturer>> GetAllManufacturersAsync();
}
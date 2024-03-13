using CabTap.Core.Entities;

namespace CabTap.Contracts.Repositories;

public interface IManufacturerRepository
{
    Task<IEnumerable<Manufacturer>> GetAllManufacturersAsync();
}
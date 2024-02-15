using CabTap.Core.Entities;

namespace CabTap.Contracts.Repositories;

public interface IManufacturerRepository
{
    Task<IEnumerable<Manufacturer>> GetAllManufacturers();
    Task<Manufacturer> GetManufacturerById(int id);
    Task<IEnumerable<Taxi>> GetTaxisByManufacturer(int manufacturerId);
}
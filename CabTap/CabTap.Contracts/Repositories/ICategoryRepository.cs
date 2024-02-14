using CabTap.Core.Entities;

namespace CabTap.Contracts.Repositories;

public interface ICategoryRepository
{
    Task<IEnumerable<Category>> GetAllCategories();
    Task<Category> GetCategoryById(int id);
    Task<IEnumerable<Taxi>> GetTaxisByCategory(int categoryId);
}
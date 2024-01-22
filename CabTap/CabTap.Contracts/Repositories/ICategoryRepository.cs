using CabTap.Core.Entities;

namespace CabTap.Contracts.Repositories;

public interface ICategoryRepository
{
    Task<IEnumerable<Category?>> GetAllCategoriesAsync();
    Task<Category> GetCategoryByIdAsync(int categoryId);
    Task<IEnumerable<Taxi>> GetTaxisByCategoryIdAsync(int categoryId);
}
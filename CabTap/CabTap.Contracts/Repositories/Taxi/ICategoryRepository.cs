using CabTap.Core.Entities;

namespace CabTap.Contracts.Repositories.Taxi;

public interface ICategoryRepository
{
    Task<IEnumerable<Category>> GetAllCategoriesAsync();
    Task<Category> GetCategoryByIdAsync(int id);
}
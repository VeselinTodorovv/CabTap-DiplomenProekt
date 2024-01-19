using CabTap.Core.Entities;

namespace CabTap.Contracts.Repositories;

public interface ICategoryRepository
{
    Task<IEnumerable<Category?>> GetAllCategoriesAsync();
    Task<Category> GetCategoryByIdAsync(int categoryId);
    Task AddCategoryAsync(Category category);
    Task UpdateCategoryAsync(Category category);
    Task DeleteCategoryAsync(int categoryId);
}
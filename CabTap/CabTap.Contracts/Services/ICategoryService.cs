using CabTap.Shared.Category;

namespace CabTap.Contracts.Services;

public interface ICategoryService
{
    Task<IEnumerable<CategoryPairViewModel>> GetAllCategoriesAsync();
    Task<CategoryPairViewModel> GetCategoryByIdAsync(int id);
}
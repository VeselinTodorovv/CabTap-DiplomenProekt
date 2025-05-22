using CabTap.Shared.Category;

namespace CabTap.Contracts.Services.Taxi;

public interface ICategoryService
{
    Task<IEnumerable<CategoryPairViewModel>> GetAllCategoriesAsync();
    Task<CategoryPairViewModel> GetCategoryByIdAsync(int id);
}
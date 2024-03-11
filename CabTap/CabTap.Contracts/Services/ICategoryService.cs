using CabTap.Shared.Category;

namespace CabTap.Contracts.Services;

public interface ICategoryService
{
    Task<IEnumerable<CategoryPairViewModel>> GetAllCategories();
    Task<CategoryPairViewModel> GetCategoryById(int id);
}
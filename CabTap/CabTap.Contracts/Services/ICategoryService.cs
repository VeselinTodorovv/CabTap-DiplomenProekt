using CabTap.Shared.Category;
using CabTap.Shared.Taxi;

namespace CabTap.Contracts.Services;

public interface ICategoryService
{
    Task<IEnumerable<CategoryPairViewModel>> GetAllCategories();
    Task<CategoryPairViewModel> GetCategoryById(int id);
    Task<IEnumerable<TaxiAllViewModel>> GetTaxisByCategory(int categoryId);
}
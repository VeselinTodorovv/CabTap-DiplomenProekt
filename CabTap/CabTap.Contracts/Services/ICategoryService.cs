using CabTap.Shared.Category;
using CabTap.Shared.Taxi;

namespace CabTap.Contracts.Services;

public interface ICategoryService
{
    Task<IEnumerable<CategoryAllViewModel>> GetAllCategoriesAsync();
    Task<CategoryDetailsViewModel> GetCategoryByIdAsync(int categoryId);
    Task<IEnumerable<TaxiAllViewModel>> GetTaxisByCategoryIdAsync(int categoryId);
}
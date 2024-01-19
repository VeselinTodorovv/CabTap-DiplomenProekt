using CabTap.Shared.Category;

namespace CabTap.Contracts.Services;

public interface ICategoryService
{
    Task<IEnumerable<CategoryAllViewModel>> GetAllCategoriesAsync();
    Task<CategoryDetailsViewModel> GetCategoryByIdAsync(int categoryId);
    Task AddCategoryAsync(CategoryCreateViewModel categoryViewModel);
    Task UpdateCategoryAsync(CategoryEditViewModel categoryViewModel);
    Task DeleteCategoryAsync(int categoryId);
}
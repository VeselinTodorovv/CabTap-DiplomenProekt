using CabTap.Contracts.Repositories;
using CabTap.Contracts.Services;
using CabTap.Shared.Category;

namespace CabTap.Services.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<IEnumerable<CategoryPairViewModel>> GetAllCategoriesAsync()
    {
        var categories = await _categoryRepository.GetAllCategoriesAsync();

        var model = categories.Select(x => new CategoryPairViewModel
        {
            Id = x.Id,
            Name = x.Name,
            Rate = x.Rate
        });

        return model;
    }

    public async Task<CategoryPairViewModel> GetCategoryByIdAsync(int id)
    {
        var category = await _categoryRepository.GetCategoryByIdAsync(id);

        var model = new CategoryPairViewModel
        {
            Id = category.Id,
            Name = category.Name,
            Rate = category.Rate
        };

        return model;
    }
}
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

    public async Task<IEnumerable<CategoryPairViewModel>> GetAllCategories()
    {
        var categories = await _categoryRepository.GetAllCategories();

        var model = categories.Select(x => new CategoryPairViewModel
        {
            Id = x.Id,
            Name = x.Name,
            Rate = x.Rate
        });

        return model;
    }

    public async Task<CategoryPairViewModel> GetCategoryById(int id)
    {
        var category = await _categoryRepository.GetCategoryById(id);

        var model = new CategoryPairViewModel
        {
            Id = category.Id,
            Name = category.Name,
            Rate = category.Rate
        };

        return model;
    }
}
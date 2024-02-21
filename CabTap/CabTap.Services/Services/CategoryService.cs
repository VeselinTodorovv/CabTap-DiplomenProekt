using AutoMapper;
using CabTap.Contracts.Repositories;
using CabTap.Contracts.Services;
using CabTap.Shared.Category;
using CabTap.Shared.Taxi;

namespace CabTap.Services.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CategoryPairViewModel>> GetAllCategories()
    {
        var categories = await _categoryRepository.GetAllCategories();

        var model = categories.Select(x => new CategoryPairViewModel
        {
            Id = x.Id,
            Name = x.Name
        });

        return model;
    }

    public async Task<CategoryPairViewModel> GetCategoryById(int id)
    {
        var category = await _categoryRepository.GetCategoryById(id);

        var model = new CategoryPairViewModel
        {
            Id = category.Id,
            Name = category.Name
        };

        return model;
    }

    public async Task<IEnumerable<TaxiAllViewModel>> GetTaxisByCategory(int categoryId)
    {
        var taxisByCategory = await _categoryRepository.GetTaxisByCategory(categoryId);

        var model = _mapper.Map<IEnumerable<TaxiAllViewModel>>(taxisByCategory);

        return model;
    }
}
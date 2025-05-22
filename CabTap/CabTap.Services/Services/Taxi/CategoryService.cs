using AutoMapper;
using CabTap.Contracts.Repositories.Taxi;
using CabTap.Contracts.Services.Taxi;
using CabTap.Shared.Category;

namespace CabTap.Services.Services.Taxi;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CategoryPairViewModel>> GetAllCategoriesAsync()
    {
        var categories = await _categoryRepository.GetAllCategoriesAsync();

        var model = _mapper.Map<IEnumerable<CategoryPairViewModel>>(categories);

        return model;
    }

    public async Task<CategoryPairViewModel> GetCategoryByIdAsync(int id)
    {
        var category = await _categoryRepository.GetCategoryByIdAsync(id);

        var model = _mapper.Map<CategoryPairViewModel>(category);

        return model;
    }
}
using CabTap.Contracts.Repositories;
using CabTap.Contracts.Services;
using CabTap.Shared.Category;
using CabTap.Shared.Taxi;

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
                Name = x.Name
            })
            .ToList();

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

        var model = taxisByCategory.Select(x => new TaxiAllViewModel
            {
                Id = x.Id,
                ManufacturerId = x.ManufacturerId,
                RegNumber = x.RegNumber,
                Description = x.Description,
                Picture = x.Picture,
                TaxiStatus = x.TaxiStatus,
                PassengerSeats = x.PassengerSeats,
                DriverId = x.DriverId,
                CategoryId = x.CategoryId,
                
                CreatedBy = x.CreatedBy,
                CreatedOn = x.CreatedOn,
                LastModifiedBy = x.LastModifiedBy,
                LastModifiedOn = x.LastModifiedOn,
            })
            .ToList();

        return model;
    }
}
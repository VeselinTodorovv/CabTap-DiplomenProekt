using CabTap.Contracts.Repositories;
using CabTap.Contracts.Services;
using CabTap.Core.Entities;
using CabTap.Shared.Category;
using CabTap.Shared.Taxi;
using Microsoft.AspNetCore.Http;

namespace CabTap.Services.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<IEnumerable<CategoryAllViewModel>> GetAllCategoriesAsync()
    {
        var categories = await _categoryRepository.GetAllCategoriesAsync();

        var categoryViewModels = categories.Select(category => new CategoryAllViewModel
        {
            Id = category!.Id,
            Name = category.Name,
            CreatedBy = category.CreatedBy,
            CreatedOn = category.CreatedOn,
            LastModifiedBy = category.LastModifiedBy,
            LastModifiedOn = category.LastModifiedOn
        })
            .ToList();

        return categoryViewModels;
    }

    public async Task<CategoryDetailsViewModel> GetCategoryByIdAsync(int categoryId)
    {
        var category = await _categoryRepository.GetCategoryByIdAsync(categoryId);

        var model = new CategoryDetailsViewModel
        {
            Id = category.Id,
            Name = category.Name,
            CreatedBy = category.CreatedBy,
            CreatedOn = category.CreatedOn,
            LastModifiedBy = category.LastModifiedBy,
            LastModifiedOn = category.LastModifiedOn
        };

        return model;
    }

    public async Task<IEnumerable<TaxiAllViewModel>> GetTaxisByCategoryIdAsync(int categoryId)
    {
        var taxis = await _categoryRepository.GetTaxisByCategoryIdAsync(categoryId);

        var model = taxis.Select(x => new TaxiAllViewModel
        {
            Id = x.Id,
            Manufacturer = x.Manufacturer,
            Description = x.Description,
            Picture = x.Picture,
            CategoryId = x.CategoryId,
            CreatedBy = x.CreatedBy,
            CreatedOn = x.CreatedOn,
            DriverId = x.DriverId,
            PassengerSeats = x.PassengerSeats,
            RegNumber = x.RegNumber,
            TaxiStatus = x.TaxiStatus,
            LastModifiedBy = x.LastModifiedBy,
            LastModifiedOn = x.LastModifiedOn
        });

        return model;
    }
}
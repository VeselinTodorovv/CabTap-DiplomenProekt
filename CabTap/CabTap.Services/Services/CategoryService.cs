using CabTap.Contracts.Repositories;
using CabTap.Contracts.Services;
using CabTap.Core.Entities;
using CabTap.Shared.Category;
using Microsoft.AspNetCore.Http;

namespace CabTap.Services.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IHttpContextAccessor _contextAccessor;

    public CategoryService(ICategoryRepository categoryRepository, IHttpContextAccessor contextAccessor)
    {
        _categoryRepository = categoryRepository;
        _contextAccessor = contextAccessor;
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

    public async Task AddCategoryAsync(CategoryCreateViewModel categoryViewModel)
    {
        var user = _contextAccessor.HttpContext.User.Identity?.Name;

        if (user != null)
        {
            var category = new Category
            {
                Id = categoryViewModel.Id,
                Name = categoryViewModel.Name,
                
                CreatedBy = user,
                CreatedOn = categoryViewModel.CreatedOn,
                LastModifiedBy = user,
                LastModifiedOn = DateTime.Now
            };

            await _categoryRepository.AddCategoryAsync(category);
        }
    }

    public async Task UpdateCategoryAsync(CategoryEditViewModel categoryViewModel)
    {
        var user = _contextAccessor.HttpContext.User.Identity?.Name;

        if (user != null)
        {
            var category = new Category
            {
                Id = categoryViewModel.Id,
                Name = categoryViewModel.Name,
                
                CreatedBy = categoryViewModel.CreatedBy,
                CreatedOn = categoryViewModel.CreatedOn,
                LastModifiedBy = user,
                LastModifiedOn = DateTime.Now
            };
        
            await _categoryRepository.UpdateCategoryAsync(category);
        }
    }

    public async Task DeleteCategoryAsync(int categoryId)
    {
        await _categoryRepository.DeleteCategoryAsync(categoryId);
    }
}
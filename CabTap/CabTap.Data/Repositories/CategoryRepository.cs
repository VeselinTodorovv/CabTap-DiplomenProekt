using CabTap.Contracts.Repositories;
using CabTap.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CabTap.Data.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly ApplicationDbContext _context;
    
    public CategoryRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<Category?>> GetAllCategoriesAsync()
    {
        return await _context.Categories.ToListAsync();
    }

    public async Task<Category> GetCategoryByIdAsync(int categoryId)
    {
        return await _context.Categories.FindAsync(categoryId);
    }

    public async Task AddCategoryAsync(Category category)
    {
        _context.Categories.Add(category);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateCategoryAsync(Category category)
    {
        var existingCategory = await _context.Categories.FindAsync(category.Id);

        if (existingCategory != null)
        {
            _context.Entry(existingCategory).CurrentValues.SetValues(category);
            _context.Entry(existingCategory).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }

    public async Task DeleteCategoryAsync(int categoryId)
    {
        var categoryToRemove = await _context.Categories.FindAsync(categoryId);
        if (categoryToRemove != null)
        {
            _context.Categories.Remove(categoryToRemove);
            await _context.SaveChangesAsync();
        }
    }
}
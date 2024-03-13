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

    public async Task<IEnumerable<Category>> GetAllCategories()
    {
        return await _context.Categories.ToListAsync();
    }

    public async Task<Category> GetCategoryById(int id)
    {
        var category = await _context.Categories.FindAsync(id);
        if (category == null)
        {
            throw new InvalidOperationException($"Category with id {id} not found.");
        }

        return category;
    }
}
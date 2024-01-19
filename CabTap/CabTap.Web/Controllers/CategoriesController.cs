using CabTap.Contracts.Services;
using Microsoft.AspNetCore.Mvc;

namespace CabTap.Web.Controllers;

public class CategoriesController : Controller
{
    private readonly ICategoryService _categoryService;
    
    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }
}
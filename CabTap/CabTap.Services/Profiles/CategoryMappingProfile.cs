using AutoMapper;
using CabTap.Core.Entities;
using CabTap.Shared.Category;

namespace CabTap.Services.Profiles;

public class CategoryMappingProfile : Profile
{
    public CategoryMappingProfile()
    {
        CreateMap<Category, CategoryPairViewModel>();
    }
}
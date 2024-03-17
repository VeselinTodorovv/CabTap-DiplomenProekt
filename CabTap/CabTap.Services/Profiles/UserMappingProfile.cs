using AutoMapper;
using CabTap.Core.Entities;
using CabTap.Shared.User;

namespace CabTap.Services.Profiles;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<ApplicationUser, ClientDetailsViewModel>();
        CreateMap<ApplicationUser, ClientIndexViewModel>();
        CreateMap<ApplicationUser, ClientDeleteViewModel>();
    }
}
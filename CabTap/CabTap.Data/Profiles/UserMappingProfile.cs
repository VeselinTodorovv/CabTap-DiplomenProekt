using AutoMapper;
using CabTap.Core.Entities;
using CabTap.Shared.User;

namespace CabTap.Data.Profiles;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<ApplicationUser, ClientDetailsViewModel>();
    }
}
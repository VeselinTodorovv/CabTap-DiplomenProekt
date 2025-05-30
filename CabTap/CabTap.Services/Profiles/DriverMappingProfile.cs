using AutoMapper;
using CabTap.Core.Entities;
using CabTap.Shared.Driver;

namespace CabTap.Services.Profiles;

public class DriverMappingProfile : Profile
{
    public DriverMappingProfile()
    {
        CreateMap<Driver, DriverAllViewModel>();
        CreateMap<Driver, DriverDetailsViewModel>();
        CreateMap<Driver, DriverDeleteViewModel>();
        CreateMap<Driver, DriverCreateViewModel>().ReverseMap();
        CreateMap<Driver, DriverEditViewModel>().ReverseMap();
        
        CreateMap<DriverDetailsViewModel, DriverDeleteViewModel>();
        CreateMap<DriverDetailsViewModel, DriverEditViewModel>();
    }
}
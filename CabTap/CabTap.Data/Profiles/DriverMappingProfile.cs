using AutoMapper;
using CabTap.Core.Entities;
using CabTap.Shared.Driver;

namespace CabTap.Data.Profiles;

public class DriverMappingProfile : Profile
{
    public DriverMappingProfile()
    {
        CreateMap<Driver, DriverAllViewModel>().ReverseMap();
        CreateMap<Driver, DriverDetailsViewModel>().ReverseMap();
        CreateMap<Driver, DriverCreateViewModel>().ReverseMap();
        CreateMap<Driver, DriverEditViewModel>().ReverseMap();
        CreateMap<Driver, DriverDeleteViewModel>().ReverseMap();

        CreateMap<DriverDetailsViewModel, DriverEditViewModel>();
        CreateMap<DriverDetailsViewModel, DriverDeleteViewModel>();
    }
}
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
        
        CreateMap<Driver, DriverEditViewModel>().ReverseMap()
            .ForMember(dest => dest.CreatedOn, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
            .ForMember(dest => dest.LastModifiedOn, opt => opt.Ignore())
            .ForMember(dest => dest.LastModifiedBy, opt => opt.Ignore());
        
        CreateMap<DriverDetailsViewModel, DriverDeleteViewModel>();
    }
}
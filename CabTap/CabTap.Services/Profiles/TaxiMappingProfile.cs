using AutoMapper;
using CabTap.Core.Entities;
using CabTap.Shared.Taxi;

namespace CabTap.Services.Profiles;

public class TaxiMappingProfile : Profile
{
    public TaxiMappingProfile()
    {
        CreateMap<Taxi, TaxiAllViewModel>();
        CreateMap<Taxi, TaxiDetailsViewModel>();
        CreateMap<Taxi, TaxiDeleteViewModel>();
        CreateMap<Taxi, TaxiCreateViewModel>().ReverseMap();
        
        CreateMap<Taxi, TaxiEditViewModel>().ReverseMap()
            .ForMember(dest => dest.CreatedOn, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
            .ForMember(dest => dest.LastModifiedOn, opt => opt.Ignore())
            .ForMember(dest => dest.LastModifiedBy, opt => opt.Ignore());


        CreateMap<TaxiDetailsViewModel, TaxiEditViewModel>();
        CreateMap<TaxiDetailsViewModel, TaxiDeleteViewModel>();
    }
}
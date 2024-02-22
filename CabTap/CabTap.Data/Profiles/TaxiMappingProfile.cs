using AutoMapper;
using CabTap.Core.Entities;
using CabTap.Shared.Taxi;

namespace CabTap.Data.Profiles;

public class TaxiMappingProfile : Profile
{
    public TaxiMappingProfile()
    {
        CreateMap<Taxi, TaxiAllViewModel>().ReverseMap();
        CreateMap<Taxi, TaxiDetailsViewModel>().ReverseMap();
        CreateMap<Taxi, TaxiCreateViewModel>().ReverseMap();
        CreateMap<Taxi, TaxiEditViewModel>().ReverseMap();
        CreateMap<Taxi, TaxiDeleteViewModel>().ReverseMap();

        CreateMap<TaxiDetailsViewModel, TaxiEditViewModel>();
        CreateMap<TaxiDetailsViewModel, TaxiDeleteViewModel>();
    }
}
using AutoMapper;
using CabTap.Core.Entities;
using CabTap.Shared.Taxi;

namespace CabTap.Data.Profiles;

public class TaxiMappingProfile : Profile
{
    public TaxiMappingProfile()
    {
        CreateMap<Taxi, TaxiAllViewModel>();
        CreateMap<Taxi, TaxiDetailsViewModel>();
        CreateMap<Taxi, TaxiDeleteViewModel>();
        CreateMap<Taxi, TaxiCreateViewModel>().ReverseMap();
        CreateMap<Taxi, TaxiEditViewModel>().ReverseMap();

        CreateMap<TaxiDetailsViewModel, TaxiEditViewModel>();
        CreateMap<TaxiDetailsViewModel, TaxiDeleteViewModel>();
    }
}
using AutoMapper;
using CabTap.Core.Entities;
using CabTap.Shared.Reservation;

namespace CabTap.Data.Profiles;

public class ReservationMappingProfile : Profile
{
    public ReservationMappingProfile()
    {
        CreateMap<Reservation, ReservationAllViewModel>().ReverseMap();
        CreateMap<Reservation, ReservationDetailsViewModel>().ReverseMap();
        CreateMap<Reservation, ReservationCreateViewModel>().ReverseMap();
        CreateMap<Reservation, ReservationEditViewModel>().ReverseMap();
        CreateMap<Reservation, ReservationDeleteViewModel>().ReverseMap();

        CreateMap<ReservationDetailsViewModel, ReservationEditViewModel>();
        CreateMap<ReservationDetailsViewModel, ReservationDeleteViewModel>();
    }
}
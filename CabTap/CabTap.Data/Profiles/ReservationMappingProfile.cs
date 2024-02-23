using AutoMapper;
using CabTap.Core.Entities;
using CabTap.Shared.Reservation;

namespace CabTap.Data.Profiles;

public class ReservationMappingProfile : Profile
{
    public ReservationMappingProfile()
    {
        CreateMap<Reservation, ReservationAllViewModel>();
        CreateMap<Reservation, ReservationDetailsViewModel>();
        CreateMap<Reservation, ReservationDeleteViewModel>();
        CreateMap<Reservation, ReservationCreateViewModel>().ReverseMap();
        CreateMap<Reservation, ReservationEditViewModel>().ReverseMap();

        CreateMap<ReservationDetailsViewModel, ReservationEditViewModel>();
        CreateMap<ReservationDetailsViewModel, ReservationDeleteViewModel>();
    }
}
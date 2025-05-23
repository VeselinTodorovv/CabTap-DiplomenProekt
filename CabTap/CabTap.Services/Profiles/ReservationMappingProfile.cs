using AutoMapper;
using CabTap.Core.Entities;
using CabTap.Shared.Reservation;
using NetTopologySuite.Geometries;

namespace CabTap.Services.Profiles;

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

        CreateMap<ReservationCreateViewModel, Reservation>()
            .ForMember(r => r.OriginPoint, opt => opt.MapFrom(src =>
                ConvertToPoint(src.OriginPoint)))
            .ForMember(r => r.DestinationPoint, opt => opt.MapFrom(src =>
                ConvertToPoint(src.DestinationPoint)));
    }
    
    private static Point ConvertToPoint(string pointString)
    {
        if (string.IsNullOrEmpty(pointString))
            return new Point(0, 0); // or new Point(0, 0) if you want a default

        // For example, pointString = "23.1234,42.1234"
        var parts = pointString.Split(',');
        if (parts.Length != 2)
        {
            return null;
        }

        if (double.TryParse(parts[0], out var x) &&
            double.TryParse(parts[1], out var y))
        {
            return new Point(x, y) { SRID = 4326 };
        }

        return null;
    }
}
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
        CreateMap<Reservation, ReservationDeleteViewModel>();
        CreateMap<Reservation, ReservationCreateViewModel>().ReverseMap();
        
        CreateMap<Reservation, ReservationEditViewModel>().ReverseMap()
            .ForMember(dest => dest.OriginPoint, opt =>
                opt.MapFrom(src => ConvertToPoint(src.OriginPoint)))
            .ForMember(dest => dest.DestinationPoint, opt =>
                opt.MapFrom(src => ConvertToPoint(src.DestinationPoint)));

        CreateMap<Reservation, ReservationDetailsViewModel>()
            .ForMember(dest => dest.OriginPoint, opt =>
                opt.MapFrom(src => ConvertToString(src.OriginPoint)))
            .ForMember(dest => dest.DestinationPoint, opt =>
                opt.MapFrom(src => ConvertToString(src.DestinationPoint)));

        CreateMap<ReservationDetailsViewModel, ReservationEditViewModel>();
        CreateMap<ReservationDetailsViewModel, ReservationDeleteViewModel>();

        CreateMap<ReservationCreateViewModel, Reservation>()
            .ForMember(r => r.OriginPoint, opt =>
                opt.MapFrom(src => ConvertToPoint(src.OriginPoint)))
            .ForMember(r => r.DestinationPoint, opt =>
                opt.MapFrom(src => ConvertToPoint(src.DestinationPoint)));
    }
    
    private static Point? ConvertToPoint(string pointString)
    {
        if (string.IsNullOrEmpty(pointString))
        {
            return new Point(0, 0);
        }

        var parts = pointString.Split(',');
        if (parts.Length != 2)
        {
            return null;
        }

        if (double.TryParse(parts[0], out var y) &&
            double.TryParse(parts[1], out var x))
        {
            return new Point(x, y) { SRID = 4326 };
        }

        return null;
    }

    private static string? ConvertToString(Point? point)
    {
        return point == null
            ? null
            : $"{point.Y},{point.X}";
    }
}
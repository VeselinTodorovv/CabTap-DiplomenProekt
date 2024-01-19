using CabTap.Contracts.Services;
using Microsoft.AspNetCore.Mvc;

namespace CabTap.Web.Controllers;

public class ReservationsController : Controller
{
    private readonly IReservationService _reservationService;
    
    public ReservationsController(IReservationService reservationService)
    {
        _reservationService = reservationService;
    }
}
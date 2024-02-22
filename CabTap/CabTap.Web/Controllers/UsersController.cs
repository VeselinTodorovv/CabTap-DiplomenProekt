using CabTap.Contracts.Services;

using Microsoft.AspNetCore.Mvc;

namespace CabTap.Web.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _userService.GetAllClientsAsync();

            return View(users);
        }
    }
}

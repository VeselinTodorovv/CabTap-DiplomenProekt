using CabTap.Api.Contracts;
using CabTap.Shared.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CabTap.Api.Controllers;

[Route("[controller]")]
[AllowAnonymous]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;

    public AuthController(IUserService userService)
    {
        _userService = userService;
    }

    // /auth/register
    [HttpPost("register")]
    public async Task<IActionResult> RegisterAsync([FromBody]RegisterViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest("Invalid model"); // Status code 400
        }

        var result = await _userService.RegisterUserAsync(model);

        if (result.IsSuccessful)
        {
            return Ok(result); // Status code 200
        }

        return BadRequest(result);
    }
}
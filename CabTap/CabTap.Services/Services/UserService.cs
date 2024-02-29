using CabTap.Contracts.Services;
using CabTap.Core.Entities;
using CabTap.Shared.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace CabTap.Services.Services;

public class UserService : IUserService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserService(UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor)
    {
        _userManager = userManager;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<ApplicationUser?> GetCurrentUserAsync()
    {
        var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);

        return user ?? throw new InvalidOperationException("Unable to get user.");
    }

    public async Task<IEnumerable<ClientIndexViewModel>> GetAllClientsAsync()
    {
        var users = _userManager.Users
            .Select(x => new ClientIndexViewModel
            {
                Id = x.Id,
                UserName = x.UserName,
                Email = x.Email,
                Address = x.Address,
                FirstName = x.FirstName,
                LastName = x.LastName,
            })
            .ToList();

        var adminIds = (await _userManager.GetUsersInRoleAsync("Administrator")).Select(u => u.Id).ToArray();
        foreach (var user in users)
        {
            user.IsAdmin = adminIds.Contains(user.Id);
        }

        var list = users
            .Where(x => !x.IsAdmin)
            .OrderBy(u => u.UserName);

        return list;
    }
}
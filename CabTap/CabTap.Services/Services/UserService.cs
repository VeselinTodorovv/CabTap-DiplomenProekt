using AutoMapper;

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
    private readonly IMapper _mapper;

    public UserService(UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor, IMapper mapper)
    {
        _userManager = userManager;
        _httpContextAccessor = httpContextAccessor;
        _mapper = mapper;
    }

    public async Task<string?> GetCurrentUserName()
    {
        var userName = (await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User))?.UserName;
        if (userName == null)
        {
            throw new InvalidOperationException("Unable to get username.");
        }

        return userName;
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

        users = users.Where(x => !x.IsAdmin).OrderBy(u => u.UserName).ToList();

        return users;
    }
}
using AutoMapper;
using CabTap.Contracts.Services;
using CabTap.Core.Entities;
using CabTap.Shared.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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

    public async Task<ApplicationUser?> GetCurrentUserAsync()
    {
        var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);

        return user;
    }

    public async Task<IEnumerable<ClientIndexViewModel>> GetPaginatedClientsAsync(int page, int pageSize)
    {
        var adminIds = (await _userManager.GetUsersInRoleAsync("Administrator"))
            .Select(u => u.Id);

        var paginatedUsers = _userManager.Users
            .Where(u => !adminIds.Contains(u.Id)) // Exclude admins
            .Skip((page - 1) * pageSize)
            .Take(pageSize);

        var clientViewModels = _mapper.Map<List<ClientIndexViewModel>>(paginatedUsers);

        return clientViewModels;
    }

    public async Task<ClientDetailsViewModel> GetClientDetailsByIdAsync(string id)
    {
        var user = await _userManager.Users
            .FirstOrDefaultAsync(x => x.Id == id);
        if (user == null)
        {
            throw new InvalidOperationException($"User with ID '{id}' not found.");
        }

        var clientDetails = _mapper.Map<ClientDetailsViewModel>(user);

        return clientDetails;
    }

    public async Task<ClientDeleteViewModel> GetClientToDeleteByIdAsync(string id)
    {
        var user = await _userManager.Users
            .FirstOrDefaultAsync(x => x.Id == id);
        if (user == null)
        {
            throw new InvalidOperationException($"User with ID '{id}' not found.");
        }

        var client = _mapper.Map<ClientDeleteViewModel>(user);

        return client;
    }

    public async Task DeleteClientAsync(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null)
        {
            throw new InvalidOperationException($"User with ID '{id}' not found.");
        }
        
        await _userManager.DeleteAsync(user);
    }
}
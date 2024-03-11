using CabTap.Core.Entities;
using CabTap.Shared.User;

namespace CabTap.Contracts.Services;

public interface IUserService
{
    Task<IEnumerable<ClientIndexViewModel>> GetPaginatedClientsAsync(int page, int pageSize);
    Task<ApplicationUser?> GetCurrentUserAsync();
    Task<ClientDetailsViewModel> GetClientDetailsAsync(string id);
}
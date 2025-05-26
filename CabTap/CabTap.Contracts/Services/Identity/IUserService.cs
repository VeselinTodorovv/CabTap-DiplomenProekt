using CabTap.Core.Entities;
using CabTap.Shared.User;

namespace CabTap.Contracts.Services.Identity;

public interface IUserService
{
    Task<IEnumerable<ClientIndexViewModel>> GetPaginatedClientsAsync(int page, int pageSize);
    Task<ApplicationUser> GetCurrentUserAsync();
    Task<ClientDetailsViewModel> GetClientDetailsByIdAsync(string id);
    Task<ClientDeleteViewModel> GetClientToDeleteByIdAsync(string id);
    Task DeleteClientAsync(string id);
    Task<string?> GetUserId(string searchInput);
}
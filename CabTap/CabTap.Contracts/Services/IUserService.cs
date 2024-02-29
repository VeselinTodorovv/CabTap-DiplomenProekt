using CabTap.Core.Entities;
using CabTap.Shared.User;

namespace CabTap.Contracts.Services;

public interface IUserService
{
    Task<ApplicationUser?> GetCurrentUserAsync();
    Task<IEnumerable<ClientIndexViewModel>> GetAllClientsAsync();
}
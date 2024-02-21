using CabTap.Shared.User;

namespace CabTap.Contracts.Services;

public interface IUserService
{
    Task<string?> GetCurrentUserName();
    Task<IEnumerable<ClientIndexViewModel>> GetAllClientsAsync();
}
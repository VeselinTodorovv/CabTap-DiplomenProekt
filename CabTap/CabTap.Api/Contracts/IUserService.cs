using CabTap.Shared.Authentication;

namespace CabTap.Api.Contracts
{
    public interface IUserService
    {
        internal Task<UserManagerResponse> RegisterUserAsync();
        internal Task<UserManagerResponse> LoginUserAsync();
    }
}
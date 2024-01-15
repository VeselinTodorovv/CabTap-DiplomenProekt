using CabTap.Shared.Authentication;

namespace CabTap.Api.Contracts
{
    public interface IUserService
    {
        public Task<UserManagerResponse> RegisterUserAsync(RegisterViewModel model);
        public Task<UserManagerResponse> LoginUserAsync(LoginViewModel model);
    }
}
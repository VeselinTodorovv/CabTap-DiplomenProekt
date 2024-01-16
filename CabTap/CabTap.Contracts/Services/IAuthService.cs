namespace CabTap.Contracts.Services;

public interface IAuthService
{
    /// <summary>
    /// Log in the user with the specified username and token.
    /// </summary>
    /// <param name="userName"></param>
    /// <param name="token"></param>
    public Task SignInUserAsync(string userName, string token);
}
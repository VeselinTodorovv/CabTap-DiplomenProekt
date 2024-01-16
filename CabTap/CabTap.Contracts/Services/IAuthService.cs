namespace CabTap.Contracts.Services;

public interface IAuthService
{
    /// <summary>
    /// Log in the user with the specified username and token.
    /// </summary>
    /// <param name="userName"></param>
    /// <param name="token"></param>
    public Task SignInUserAsync(string userName, string token);

    /// <summary>
    /// Sign out the current user.
    /// </summary>
    /// <returns></returns>
    public Task SignOutUserAsync();
}
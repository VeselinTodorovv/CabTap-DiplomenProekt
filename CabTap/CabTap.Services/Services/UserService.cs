using CabTap.Contracts.Services;
using Microsoft.AspNetCore.Http;

namespace CabTap.Services.Services;

public class UserService : IUserService
{
    private readonly IHttpContextAccessor _contextAccessor;

    public UserService(IHttpContextAccessor contextAccessor)
    {
        _contextAccessor = contextAccessor;
    }

    public string? GetCurrentUserName()
    {
        return _contextAccessor.HttpContext.User.Identity?.Name;
    }
}
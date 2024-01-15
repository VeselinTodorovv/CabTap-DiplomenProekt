using CabTap.Api.Contracts;
using CabTap.Api.Models;
using CabTap.Shared.Authentication;
using Microsoft.AspNetCore.Identity;

namespace CabTap.Api.Services;

public class UserService : IUserService
{
    private readonly UserManager<ApplicationUser> _userManager;

    public UserService(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<UserManagerResponse> RegisterUserAsync(RegisterViewModel model)
    {
        if (model == null)
        {
            throw new NullReferenceException("Model is null");
        }

        if (model.Password != model.ConfirmPassword)
        {
            return new UserManagerResponse
            {
                Message = "Passwords do not match",
                IsSuccessful = false
            };
        }

        var user = new ApplicationUser
        {
            Address = model.Address,
            Email = model.Email,
            FirstName = model.FirstName,
            LastName = model.LastName,
            PhoneNumber = model.PhoneNumber,
            UserName = model.UserName
        };

        var result = await _userManager.CreateAsync(user, model.Password);

        if (!result.Succeeded)
        {
            return new UserManagerResponse 
            {
                Message = "User was not created",
                IsSuccessful = false,
                Errors = result.Errors.Select(error => error.Description)
            };
        }

        await _userManager.AddToRoleAsync(user, "Client");
        
        // TODO: Send a confirmation email
        return new UserManagerResponse
        {
            Message = "User created successfully",
            IsSuccessful = true
        };
    }

    public Task<UserManagerResponse> LoginUserAsync(LoginViewModel model)
    {
        throw new NotImplementedException();
    }
}
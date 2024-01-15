using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using CabTap.Api.Contracts;
using CabTap.Api.Models;
using CabTap.Shared.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace CabTap.Api.Services;

public class UserService : IUserService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IConfiguration _configuration;

    public UserService(UserManager<ApplicationUser> userManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _configuration = configuration;
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
                IsSuccessful = false,
                StatusCode = HttpStatusCode.BadRequest
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
                Errors = result.Errors.Select(error => error.Description),
                StatusCode = HttpStatusCode.BadRequest
            };
        }

        await _userManager.AddToRoleAsync(user, "Client");
        
        // TODO: Send a confirmation email
        return new UserManagerResponse
        {
            Message = "User created successfully",
            IsSuccessful = true,
            StatusCode = HttpStatusCode.Created
        };
    }
    
    public async Task<UserManagerResponse> LoginUserAsync(LoginViewModel model)
    {
        var authSettings = _configuration.GetSection("AuthSettings"); 
        
        var user = await _userManager.FindByNameAsync(model.UserName);
        if (user == null)
        {
            return new UserManagerResponse
            {
                Message = $"User with username: {model.UserName} was not found",
                IsSuccessful = false,
                StatusCode = HttpStatusCode.NotFound
            };
        }

        var result = await _userManager.CheckPasswordAsync(user, model.Password);
        if (!result)
        {
            return new UserManagerResponse
            {
                Message = "Invalid password",
                IsSuccessful = false,
                StatusCode = HttpStatusCode.BadRequest
            };
        }

        var claims = new[]
        {
            new Claim("UserName", model.UserName),
            new Claim(ClaimTypes.NameIdentifier, user.Id)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authSettings["Key"]));

        var token = new JwtSecurityToken(
            authSettings["Issuer"],
            authSettings["Audience"],
            claims,
            expires: DateTime.Now.AddDays(30),
            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
        );

        var tokenAsString = new JwtSecurityTokenHandler().WriteToken(token);

        return new UserManagerResponse
        {
            Message = "Authentication Successful",
            IsSuccessful = true,
            Token = tokenAsString,
            ExpireDate = token.ValidTo,
            StatusCode = HttpStatusCode.OK
        };
    }
}
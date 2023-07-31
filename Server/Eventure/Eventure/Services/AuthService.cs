using Eventure.Models.Entities;
using Eventure.Models.RequestDto;
using Microsoft.AspNetCore.Identity;

namespace Eventure.Services;

public class AuthService:IAuthService
{
    private readonly UserManager<User> _userManager;

    public AuthService(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<bool> RegisterUser(RegisterUserDto registerUserDto)
    {
        var user = new User()
        {
            UserName = registerUserDto.Username,
            Email = registerUserDto.Email
        };
        var registerResult = await _userManager.CreateAsync(user, registerUserDto.Password);
       
            return registerResult.Succeeded;
        
        
    }
}
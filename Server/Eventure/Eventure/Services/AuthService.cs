using Eventure.Models.Entities;
using Eventure.Models.RequestDto;
using Eventure.Models.Results;
using Microsoft.AspNetCore.Identity;

namespace Eventure.Services;

public class AuthService:IAuthService
{
    private readonly UserManager<User> _userManager;

    public AuthService(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<RegisterResult> RegisterUser(RegisterUserDto registerUserDto)
    {
        var user = new User()
        {
            UserName = registerUserDto.Username,
            Email = registerUserDto.Email
        };
        var registerResult = await _userManager.CreateAsync(user, registerUserDto.Password);
        if (registerResult.Succeeded)
        {
            return (new RegisterResult()
            {
                Succeeded = true,
                Message = new List<string>{
                "Registration successful"
                }
            });
        }
            return (new RegisterResult()
            {
                Succeeded = false,
                Message = registerResult.Errors.Select(e => e.Description).ToList()
            });
        
        
    }
}
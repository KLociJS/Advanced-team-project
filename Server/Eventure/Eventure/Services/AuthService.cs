using Eventure.Models.Entities;
using Eventure.Models.RequestDto;
using Eventure.Models.Results;
using Microsoft.AspNetCore.Identity;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;


namespace Eventure.Services;

public class AuthService:IAuthService
{
    private readonly UserManager<User> _userManager;
    private readonly IConfiguration _configuration;

    public AuthService(UserManager<User> userManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _configuration = configuration;
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
    
    public async Task<LoginResult> LoginAsync(LoginUserDto loginUserDto)
    {
        try
        {
            var user = await _userManager.FindByNameAsync(loginUserDto.UserName);
            if (user != null)
            { 
                var isPasswordValid = await _userManager.CheckPasswordAsync(user, loginUserDto.Password);
                if (isPasswordValid)
                {
                    var token = await GetJwtTokenAsync(user, loginUserDto.Password!);
                    return LoginResult.Success(token!);
                }
            }
            return LoginResult.Fail("Wrong username or password.");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception("An error occured on the server.");
        }
    }

    public async Task<string> GetJwtTokenAsync(User user, string password)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.UserName)
            // Add other claims as needed
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddDays(1), // Set token expiration time
            SigningCredentials = credentials
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var securityToken = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(securityToken);
    }


    public async Task<IList<string>> GetRolesAsync(string userName)
    {
        var user = await _userManager.FindByNameAsync(userName);
        var roles = await _userManager.GetRolesAsync(user);
        return roles;
    }

}
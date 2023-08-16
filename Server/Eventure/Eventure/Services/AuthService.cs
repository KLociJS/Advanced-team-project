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

public class AuthService : IAuthService
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
        try
        {
            var user = new User()
            {
                UserName = registerUserDto.Username,
                Email = registerUserDto.Email
            };
            var registerResult = await _userManager.CreateAsync(user, registerUserDto.Password);
            var asignRoleResult = await _userManager.AddToRoleAsync(user, "User");

            if (registerResult.Succeeded && asignRoleResult.Succeeded)
            {
                return (new RegisterResult()
                {
                    Succeeded = true,
                    Message = new List<string>
                    {
                        "Registration successful"
                    }
                });
            }

            if (registerResult.Succeeded)
            {
                await _userManager.DeleteAsync(user);
            }

            return (new RegisterResult()
            {
                Succeeded = false,
                Message = registerResult.Errors.Select(e => e.Description).ToList()
            });
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
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
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };
        
        var roles = await _userManager.GetRolesAsync(user);
        
        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }
        
        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
        
        var token = new JwtSecurityToken(
            issuer: _configuration["JWT:ValidIssuer"],
            audience:_configuration["JWT:ValidAudience"],
            expires: DateTime.Now.AddDays(14),
            claims:claims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }


    public async Task<IList<string>> GetRolesAsync(string userName)
    {
        var user = await _userManager.FindByNameAsync(userName);
        var roles = await _userManager.GetRolesAsync(user);
        return roles;
    }
    


}
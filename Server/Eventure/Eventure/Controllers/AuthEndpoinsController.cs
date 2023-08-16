using Eventure.Models.RequestDto;
using Eventure.Models.ResponseDto;
using Eventure.Models.Results;
using Eventure.Services;
using Microsoft.AspNetCore.Mvc;


namespace Eventure.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthEndpointsController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthEndpointsController(IAuthService authService)
    {
        _authService = authService;
    }

    //Login Endpoint
    [HttpPost]
    [Route("/api/login")]
    public async Task<IActionResult> Login([FromBody] LoginUserDto loginUserDto)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(LoginResult.Fail("Invalid inputs"));
            }

            var authResult = await _authService.LoginAsync(loginUserDto);

            if (authResult.Succeeded)
            {
                HttpContext.Response.Cookies.Append("token", authResult.Token, new CookieOptions()
                {
                    SameSite = SameSiteMode.None,
                    Expires = DateTimeOffset.Now.AddDays(14),
                    IsEssential = true,
                    Secure = true,
                    HttpOnly = true
                });

                var roles = await _authService.GetRolesAsync(loginUserDto.UserName!);

                return Ok(new LoginResponseDto
                {
                    Roles = roles,
                    UserName = loginUserDto.UserName
                });
            }

            return Unauthorized(new LoginResult { Succeeded = false, ErrorMessage = "Wrong username or password." });
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500,
                new LoginResult { Succeeded = false, ErrorMessage = "An error occured on the server." });
        }
    }


    //Logout Endpoint
    [HttpPost]
    [Route("/api/logout")]
    public async Task<IActionResult> Logout()
    {
        try
        {
            HttpContext.Response.Cookies.Append("token", "", new CookieOptions()
            {
                SameSite = SameSiteMode.None,
                Expires = DateTimeOffset.Now.AddDays(-1),
                IsEssential = true,
                Secure = true,
                HttpOnly = true
            });
            
            Console.WriteLine("Logout successful.");
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error during logout: {e}");
            return StatusCode(500, new { Message = "An error occurred during logout." });
        }
    }


    //Registration
    [HttpPost]
    [Route("/api/signup")]
    public async Task<IActionResult> Signup(RegisterUserDto registerUserDto)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(e => e.Errors.Select(err => err.ErrorMessage));
                return BadRequest(new { Errors = errors });
            }
            
            var registrationResult = await _authService.RegisterAsync(registerUserDto);

            if (registrationResult.Succeeded)
            {
                return Ok(new RegisterResponseDto
                {
                    Message = registrationResult.Message,
                    User = registerUserDto
                });
            }

            return BadRequest(new RegisterResponseDto { Message = registrationResult.Message });
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, new Response { Message = "An error occured on the server." });
        }


    }
}
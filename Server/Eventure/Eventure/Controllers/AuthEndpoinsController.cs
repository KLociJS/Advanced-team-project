using Eventure.Models.RequestDto;
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
    public async Task<IActionResult> Login()
    {
        return Ok();
    }

    
    //Logout Endpoint
    [HttpPost]
    [Route("/api/logout")]
    public async Task<IActionResult> Logout()
    {
        return Ok();
    }

    
    //Registration
    [HttpPost]
    [Route("/api/signup")]
    public async Task<IActionResult> Signup(RegisterUserDto registerUserDto)
    {
        var isRegistrationSuccessful = await _authService.RegisterUser(registerUserDto);
        if (isRegistrationSuccessful)
        {
        return Ok(new {Message = "Registered succesfully"});
        }

        return BadRequest();
    }
}
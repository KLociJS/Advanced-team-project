using Microsoft.AspNetCore.Mvc;
namespace Eventure.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthEndpointsController : ControllerBase
{
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
    public async Task<IActionResult> Signup()
    {
        return Ok("Registered successfully");
    }
}
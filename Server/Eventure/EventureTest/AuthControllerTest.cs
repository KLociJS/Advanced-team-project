using Eventure.Controllers;
using Eventure.Models.RequestDto;
using Eventure.Models.ResponseDto;
using Eventure.Models.Results;
using Eventure.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Moq;
using NUnit.Framework;

namespace EventureTest;

[TestFixture]
public class AuthControllerTest
{
    private Mock<IAuthService>? _mockAuthService;
    private AuthEndpointsController? _authController;

    [SetUp]
    public void Setup()
    {
        _mockAuthService = new Mock<IAuthService>();
        _authController = new AuthEndpointsController(_mockAuthService.Object);
    }

    [Test]
    public async Task Login_InvalidInput_ReturnsBadRequest()
    {
        var loginUserDto = new LoginUserDto();

        _authController!.ModelState.AddModelError("UserName", "Username is required.");
        _authController!.ModelState.AddModelError("Password", "Password is required.");

        var result = await _authController.Login(loginUserDto);

        Assert.IsInstanceOf<BadRequestObjectResult>(result);
    }

    [Test]
    public async Task Login_WrongCredentials_ReturnsUnauthorized()
    {
        var loginUserDto = new LoginUserDto();

        var loginResult = LoginResult.Fail("Wrong username or password.");

        _mockAuthService!.Setup(s => s.LoginAsync(It.IsAny<LoginUserDto>()))
            .ReturnsAsync(loginResult);

        var result = await _authController!.Login(loginUserDto);

        Assert.IsInstanceOf<UnauthorizedObjectResult>(result);
    }

    [Test]
    public async Task Login_ServerError_ThrowsException()
    {
        var loginUserDto = new LoginUserDto();

        _mockAuthService!.Setup(s => s.LoginAsync(It.IsAny<LoginUserDto>()))
            .ThrowsAsync(new Exception());

        var result = await _authController!.Login(loginUserDto);

        Assert.IsInstanceOf<ObjectResult>(result);
        var serverErrorResult = result as ObjectResult;
        Assert.That((serverErrorResult!.Value as LoginResult)!.ErrorMessage,
            Is.EqualTo("An error occured on the server."));
    }

    [Test]
    public async Task Register_InvalidInput_ReturnsBadRequest()
    {
        var registerUserDto = new RegisterUserDto();
        
        _authController!.ModelState.AddModelError("UserName", "Username is required.");

        var result = await _authController.Signup(registerUserDto);
        Assert.IsInstanceOf<BadRequestObjectResult>(result);
    }

    [Test]
    public async Task Register_UserNameTaken_ReturnsBadRequest()
    {
        var registerUserDto = new RegisterUserDto();
        var registerResult = new RegisterResult { Succeeded = false, Message = new List<string> { "Username already taken." }};

        _mockAuthService!.Setup(s => s.RegisterUser(It.IsAny<RegisterUserDto>()))
            .ReturnsAsync(registerResult);

        var userNameTakenResultMessage = new List<string> { "Username already taken." };

        var result = await _authController!.Signup(registerUserDto);
        Assert.IsInstanceOf<BadRequestObjectResult>(result);
        var badRequestResult = result as BadRequestObjectResult;
        Assert.That((badRequestResult!.Value as RegisterResponseDto)!.Message, Is.EqualTo(userNameTakenResultMessage));
    }

    [Test]
    public async Task Register_SuccessfulRegistration_ReturnsOkResult()
    {
        var registerUserDto = new RegisterUserDto();
        var registerResult = new RegisterResult { Succeeded = true, Message = new List<string> { "Registration successful" }};

        _mockAuthService!.Setup(s => s.RegisterUser(It.IsAny<RegisterUserDto>()))
            .ReturnsAsync(registerResult);

        var result = await _authController!.Signup(registerUserDto);
        
        Assert.IsInstanceOf<OkObjectResult>(result);
    }

    [Test]
    public async Task Register_ServerError_ReturnsStatusCode500()
    {
        var registerUserDto = new RegisterUserDto();

        _mockAuthService!.Setup(s => s.RegisterUser(It.IsAny<RegisterUserDto>()))
            .ThrowsAsync(new Exception());

        var result = await _authController!.Signup(registerUserDto);

        Assert.IsInstanceOf<ObjectResult>(result);
        var serverErrorResult = result as ObjectResult;
        Assert.That((serverErrorResult!.Value as Response)!.Message,
            Is.EqualTo("An error occured on the server."));
    }

}
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.Serialization;
using System.Security.Claims;
using Eventure.Models.Entities;
using Eventure.Models.RequestDto;
using Eventure.Models.Results;
using Eventure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using IConfiguration = Castle.Core.Configuration.IConfiguration;

namespace EventureTest;

[TestFixture]
public class AuthServiceTest
{
    private Mock<UserManager<User>> _mockUserManager;
    private Mock<IUserStore<User>> _mockUserStore;
    private Mock<Microsoft.Extensions.Configuration.IConfiguration> _configuration;
    private IAuthService _authService;

    [SetUp]
    public void Setup()
    {
        _mockUserStore = new Mock<IUserStore<User>>();
        _mockUserManager = new Mock<UserManager<User>>(_mockUserStore.Object,null,null,null,null,null,null,null,null);
        _configuration = new Mock<Microsoft.Extensions.Configuration.IConfiguration>();
        _authService = new AuthService(_mockUserManager.Object,_configuration.Object);
        
        //setting configuration fields
        _configuration.Setup(e => e["JWT:Secret"])
            .Returns("asdfghjklmnlongerthanexcepedplswork!");
    }

    [Test]
    public async Task RegisterAsync_ServerError_ThrowsException()
    {
        var registerUserDto = new RegisterUserDto();
        _mockUserManager.Setup(m => m.CreateAsync(It.IsAny<User>(), It.IsAny<string>()))
            .ThrowsAsync(new Exception("simulated exception"));

        var exception = Assert.ThrowsAsync<Exception>(async () => await _authService.RegisterAsync(registerUserDto));
        
        Assert.That(exception!.Message, Is.EqualTo("simulated exception"));

    }

    [Test]
    public async Task RegisterAsync_SuccessfullyRegistered_ReturnsSuccessfulRegisterResult()
    {
        var registerUserDto = new RegisterUserDto();
        _mockUserManager.Setup(m => m.CreateAsync(It.IsAny<User>(), It.IsAny<string>()))
            .ReturnsAsync(IdentityResult.Success);
        _mockUserManager.Setup(m => m.AddToRoleAsync(It.IsAny<User>(), It.IsAny<string>()))
            .ReturnsAsync(IdentityResult.Success);

        var result = await _authService.RegisterAsync(registerUserDto);
        
        Assert.IsInstanceOf<RegisterResult>(result);
        Assert.IsTrue(result.Succeeded);
        Assert.That(new List<string>(){"Registration successful"}, Is.EqualTo(result.Message));
    }

    [Test]
    public async Task RegisterAsync_FailedToCreateUser_ReturnsFailedRegisterResult()
    {
        var registerUserDto = new RegisterUserDto();
        _mockUserManager.Setup(m => m.CreateAsync(It.IsAny<User>(), It.IsAny<string>()))
            .ReturnsAsync(IdentityResult.Failed(new IdentityError(){Description = "User exists"}));
        _mockUserManager.Setup(m => m.AddToRoleAsync(It.IsAny<User>(), It.IsAny<string>()))
            .ReturnsAsync(IdentityResult.Failed());

        var result = await _authService.RegisterAsync(registerUserDto);
        
        Assert.IsInstanceOf<RegisterResult>(result);
        Assert.IsFalse(result.Succeeded);
        Assert.IsTrue(result.Message.Contains("User exists"));
    }

    [Test]
    public async Task LoginAsync_ServerError_ThrowsException()
    {
        var loginUserDto = new LoginUserDto();
        _mockUserManager.Setup(m => m.FindByNameAsync(It.IsAny<string>()))
            .ThrowsAsync(new Exception());

        var exception = Assert.ThrowsAsync<Exception>(async () => await _authService.LoginAsync(loginUserDto));
        
        Assert.That(exception!.Message, Is.EqualTo("An error occured on the server."));
    }

    [Test]
    public async Task LoginAsync_SuccessfullyAuthenticated_ReturnsSuccessfulLoginResult()
    {
        var user = new User() { UserName = "asd" };
        var loginUserDto = new LoginUserDto();
        var token = new JwtSecurityToken();
        var exceptedResult = LoginResult.Success(token.ToString());
        
        _mockUserManager.Setup(service => service.FindByNameAsync(It.IsAny<string>()))
            .ReturnsAsync(user);
        _mockUserManager.Setup(service => service.CheckPasswordAsync(It.IsAny<User>(), It.IsAny<string>()))
            .ReturnsAsync(true);
        _mockUserManager.Setup(service => service.GetRolesAsync(It.IsAny<User>()))
            .ReturnsAsync(new List<string>());

        var result = await _authService.LoginAsync(loginUserDto);
        
        Assert.IsInstanceOf<LoginResult>(result);
        Assert.That(result.Succeeded, Is.EqualTo(exceptedResult.Succeeded));
    }

    [Test]
    public async Task LoginAsync_UserNotFound_ReturnsFailedLoginResult()
    {
        _mockUserManager.Setup(m => m.FindByNameAsync(It.IsAny<string>()))!
            .ReturnsAsync((User)null!);

        var loginUserDto = new LoginUserDto();
        var exceptedResult = LoginResult.Fail("Wrong username or password.");
        var result = await _authService.LoginAsync(loginUserDto);
        
        Assert.That(result.Succeeded, Is.EqualTo(exceptedResult.Succeeded));
        Assert.That(result.ErrorMessage, Is.EqualTo(exceptedResult.ErrorMessage));
    }

    [Test]
    public async Task LoginAsync_PasswordDoesntMatch_ReturnsFailedLoginResult()
    {
        var user = new User();
        _mockUserManager.Setup(m => m.FindByNameAsync(It.IsAny<string>()))!
            .ReturnsAsync(user);
        _mockUserManager.Setup(m => m.CheckPasswordAsync(It.IsAny<User>(), It.IsAny<string>()))
            .ReturnsAsync(false);

        var loginUserDto = new LoginUserDto();
        var exceptedResult = LoginResult.Fail("Wrong username or password.");
        var result = await _authService.LoginAsync(loginUserDto);
        
        Assert.That(result.Succeeded, Is.EqualTo(exceptedResult.Succeeded));
        Assert.That(result.ErrorMessage, Is.EqualTo(exceptedResult.ErrorMessage));
    }

    [Test]
    public async Task GetRolesAsync_ReturnsRoles()
    {
        var user = new User();
        var roles = new List<string>() { "User", "Admin" };
        
        _mockUserManager.Setup(m => m.FindByNameAsync(It.IsAny<string>()))
            .ReturnsAsync(user);
        _mockUserManager.Setup(m => m.GetRolesAsync(It.IsAny<User>()))
            .ReturnsAsync(roles);

        var result = await _authService.GetRolesAsync("");
        
        Assert.IsTrue(result.Contains("User"));
        Assert.IsTrue(result.Contains("Admin"));

    }
    
}
using System.Security.Claims;
using Eventure.Controllers;
using Eventure.Models.RequestDto;
using Eventure.Models.ResponseDto;
using Eventure.Models.Results;
using Eventure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace EventureTest;

[TestFixture]
public class EventControllerTest
{
   
    private static readonly Mock<IEventService> _eventService = new Mock<IEventService>(); 
    private readonly EventEndpointsController _controller = new EventEndpointsController(_eventService.Object);

    [SetUp]
    public void Setup()
    {
        
    var httpContext = new DefaultHttpContext();
    httpContext.User = new ClaimsPrincipal(new ClaimsIdentity(new[]
    {
    new Claim(ClaimTypes.Name, "username")
    }));

    _controller.ControllerContext = new ControllerContext
    {
    HttpContext = httpContext
    };
    
    }
    [Test]
    public async Task CreateEvent_SuccessfulCreation_ReturnsOkResult()
    {
        var createEventDto = new CreateEventDto();
        var createResult = EventActionResult.Succeed("Event created");
        _eventService.Setup(e => e.CreateEventAsync(It.IsAny<CreateEventDto>(), It.IsAny<string>()))
            .ReturnsAsync(createResult);

        var result = await _controller.CreateEvent(createEventDto);
        
        Assert.IsInstanceOf<OkObjectResult>(result);
    }

    [Test]
    public async Task CreateEvent_FailedCreation_ReturnsBadRequest()
    {
        var createEventDto = new CreateEventDto();
        var createResult = EventActionResult.Failed("Failed to create event");
        _eventService.Setup(e => e.CreateEventAsync(It.IsAny<CreateEventDto>(), It.IsAny<string>()))
            .ReturnsAsync(createResult);

        var result = await _controller.CreateEvent(createEventDto);
        
        Assert.IsInstanceOf<BadRequestObjectResult>(result);
    }

    [Test]
    public async Task CreateEvent_ExceptionThrown_ReturnsInternalServerError()
    {
        var createEventDto = new CreateEventDto();
        _eventService.Setup(e => e.CreateEventAsync(It.IsAny<CreateEventDto>(), It.IsAny<string>()))
            .ThrowsAsync(new Exception("Something went wrong"));

        var exception = Assert.ThrowsAsync<Exception>(async () => await _controller.CreateEvent(createEventDto));
        
        Assert.AreEqual("Something went wrong", exception!.Message);
    }

    [Test]
    public async Task JoinEvent_SuccessfulJoin_ReturnsOkResult()
    {
        //Arrange
        var eventId = 123;
        var username = "testUser";
        var joinEventResult = JoinEventResult.Success();
        
        _eventService.Setup(e => e.JoinEvent(eventId, username))
            .ReturnsAsync(joinEventResult);
        
        //Act
        var result = await _controller.JoinEvent(eventId);
        
        //Assert
        Assert.IsInstanceOf<ObjectResult>(result);
    }

    [Test]
    public async Task JoinEvent_FailedToJoin_ReturnsBadRequest()
    {
        //Arrange
        var eventId = 123;
        var username = "testUser";
        var joinEventResult = JoinEventResult.EventNotFound();
        
        // Set up HttpContext
        var httpContext = new DefaultHttpContext();
        httpContext.User = new ClaimsPrincipal(new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.Name, username)
        }));

        _eventService.Setup(e => e.JoinEvent(eventId, username))
            .ReturnsAsync(joinEventResult);
        
        //Act
        var result = await _controller.JoinEvent(eventId);
        
        //Assert
        Assert.IsInstanceOf<ObjectResult>(result);
    }

    
}


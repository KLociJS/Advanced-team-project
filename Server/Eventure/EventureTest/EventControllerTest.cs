using System.Security.Claims;
using Eventure.Controllers;
using Eventure.Models.Entities;
using Eventure.Models.Enums;
using Eventure.Models.RequestDto;
using Eventure.Models.ResponseDto;
using Eventure.Models.Results;
using Eventure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Internal.Execution;
using Event = Eventure.Models.Entities.Event;

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
    
    // Create Event method //
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

    
    // JoinEvent method //
    
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
    public async Task JoinEvent_FailedToJoin_ReturnsBadRequestEventNotFound()
    {
        //Arrange
        var joinEventResult = JoinEventResult.EventNotFound();

        _eventService.Setup(e => e.JoinEvent(It.IsAny<long>(), It.IsAny<string>()))
            .ReturnsAsync(joinEventResult);
        
        //Act
        var result = await _controller.JoinEvent(It.IsAny<long>());
        
        //Assert
        Assert.IsInstanceOf<ObjectResult>(result);
        var notFoundResult = result as ObjectResult;
        Assert.That((notFoundResult!.Value as Response)!.Message, Is.EqualTo("Could not join event."));
    }

    [Test]
    public async Task JoinEvent_FailedToJoin_ReturnsStatusCode500()
    {
        var eventId = 123;
        var joinEventResult = JoinEventResult.ServerError();
        _eventService.Setup(e => e.JoinEvent(It.IsAny<long>(), It.IsAny<string>()))
            .ReturnsAsync(joinEventResult);

        var result = await _controller.JoinEvent(eventId);
        
        Assert.IsInstanceOf<ObjectResult>(result);
        var errorResult = result as ObjectResult;
        Assert.That((errorResult!.Value as Response)!.Message,Is.EqualTo("An error occured on the server."));
    }
    
    
    [Test]
    public async Task JoinEvent_ExceptionThrown_ReturnsInternalServerError()
    {
        var eventId = 123;
        var username = "testUser";
        

        _eventService.Setup(e => e.JoinEvent(It.IsAny<long>(), It.IsAny<string>()))
            .ThrowsAsync(new Exception("Something went wrong"));

        var result = await _controller.JoinEvent(eventId);
        
        Assert.IsInstanceOf<ObjectResult>(result);
        var serverErrorResult = result as ObjectResult;
        Assert.That((serverErrorResult!.Value as Response)!.Message, Is.EqualTo("An error occured on the server."));

    }
    
    //Update event method//

    [Test]
    public async Task UpdateEvent_SuccessfullyUpdated_ReturnsOkResult()
    {
        //Arrange
        var eventId = 123;
        var username = "username";
        var updateEventDto = new UpdateEventDto();

        var updateEventResult = UpdateEventResult.Success(new EventPreviewResponseDto());

        _eventService.Setup(e => e.UpdateEvent(updateEventDto, eventId, username))
            .ReturnsAsync(updateEventResult);
        
        //Act
        var result = await _controller.UpdateEvent(updateEventDto, eventId);
        
        //Assert
        Assert.IsInstanceOf<OkObjectResult>(result);
    }

    [Test]
    public async Task UpdateEvent_FailedToUpdate_ReturnsBadRequest()
    {
        var eventId = 123;
        var username = "username";
        var updateEventDto = new UpdateEventDto();

        var updateEventResult = UpdateEventResult.Fail();

        _eventService.Setup(e => e.UpdateEvent(updateEventDto, eventId, username))
            .ReturnsAsync(updateEventResult);

        var result = await _controller.UpdateEvent(updateEventDto, eventId);
        
        Assert.IsInstanceOf<BadRequestObjectResult>(result);
    }

    [Test]
    public async Task UpdateEvent_ExceptionThrown_ReturnsInternalServerError()
    {
        var eventId = 123;
        var updateEventDto = new UpdateEventDto(); // You need to create a valid DTO for testing

        _eventService.Setup(e => e.UpdateEvent(updateEventDto, eventId, It.IsAny<string>()))
            .ThrowsAsync(new Exception("Something went wrong"));

        // Act
        var result = await _controller.UpdateEvent(updateEventDto, eventId);

        // Assert
        Assert.IsInstanceOf<ObjectResult>(result);
        var serverErrorResult = result as ObjectResult;
        Assert.That((serverErrorResult!.Value as Response)!.Message, Is.EqualTo("An error occured on the server."));
    }

    //Delete event //

    [Test]
    public async Task DeleteEvent_SuccessfullyDeleted_ReturnsOkResult()
    {
        var eventId = 123;
        var username = "username";
        var deleteEventResult = DeleteEventResult.Success();

        _eventService.Setup(e => e.DeleteEvent(eventId, username))
            .ReturnsAsync(deleteEventResult);

        var result = await _controller.DeleteEvent(eventId);
        
        Assert.IsInstanceOf<OkObjectResult>(result);
    }

    [Test]
    public async Task DeleteEvent_InvalidEventId_ReturnsBadRequest()
    {
        var eventId = -1;

        var result = await _controller.DeleteEvent(eventId);
        
        Assert.IsInstanceOf<BadRequestObjectResult>(result);
        
    }

    [Test]
    public async Task DeleteEvent_EventNotFound_ReturnsNotFound()
    {
        var eventId = 123;
        var username = "username";
        var deleteEventResult = DeleteEventResult.EventNotFound();

        _eventService.Setup(e => e.DeleteEvent(eventId, username))
            .ReturnsAsync(deleteEventResult);

        var result = await _controller.DeleteEvent(eventId);
        
        Assert.IsInstanceOf<NotFoundObjectResult>(result);
        var notFoundResult = result as NotFoundObjectResult;
        Assert.That(notFoundResult!.Value, Is.EqualTo("Event not found."));
    }

    [Test]
    public async Task DeleteEvent_UserNotFound_ReturnsNotFound()
    {
        var eventId = 122;
        var username = "username";
        var deleteEventResult = DeleteEventResult.UserNotFound();

        _eventService.Setup(e => e.DeleteEvent(eventId, username))
            .ReturnsAsync(deleteEventResult);

        var result = await _controller.DeleteEvent(eventId);
        
        Assert.IsInstanceOf<NotFoundObjectResult>(result);
        var notFoundResult = result as NotFoundObjectResult;
        Assert.That(notFoundResult!.Value, Is.EqualTo("User not found."));
    }
    [Test]
    public async Task DeleteEvent_FailedToDelete_ReturnsStatusCode500()
    {
        var eventId = 123;
        var deleteEventResult = DeleteEventResult.ServerError();
        _eventService.Setup(e => e.DeleteEvent(It.IsAny<long>(), It.IsAny<string>()))
            .ReturnsAsync(deleteEventResult);

        var result = await _controller.DeleteEvent(eventId);
        
        Assert.IsInstanceOf<ObjectResult>(result);
        var errorResult = result as ObjectResult;
        Assert.NotNull(errorResult);
        Assert.NotNull(errorResult!.Value);
        var response = errorResult.Value as Response;
        Assert.NotNull(response);
        Assert.That((errorResult!.Value as Response)!.Message,Is.EqualTo("An error occured on the server."));
    }
    
    [Test]
    public async Task DeleteEvent_ExceptionThrown_ReturnsInternalServerError()
    {
        var eventId = 123;
        var username = "username";

        _eventService.Setup(e => e.DeleteEvent(eventId, username))
            .ThrowsAsync(new Exception("Something went wrong"));

        var result = await _controller.DeleteEvent(eventId);
        
        Assert.IsInstanceOf<ObjectResult>(result);
        var serverErrorResult = result as ObjectResult;
        Assert.That((serverErrorResult!.Value as Response)!.Message, Is.EqualTo("An error occured on the server."));
    }
    
    //Leave event //
    [Test]
    public async Task LeaveEvent_SuccessfullyLeft_ReturnsOkResult()
    {
        var eventId = 123;
        var username = "username";
        var leaveEventResult = LeaveEventResult.Success();

        _eventService.Setup(e => e.LeaveEvent(eventId, username))
            .ReturnsAsync(leaveEventResult);

        var result = await _controller.LeaveEvent(eventId);
        
        Assert.IsInstanceOf<OkObjectResult>(result);
    }

    [Test]
    public async Task LeaveEvent_FailedToLeave_ReturnsBadRequestEventNotFound()
    {
       
        var leaveEventResult = LeaveEventResult.EventNotFound();

        _eventService.Setup(e => e.LeaveEvent(It.IsAny<long>(), It.IsAny<string>()))
            .ReturnsAsync(leaveEventResult);

        var result = await _controller.LeaveEvent(It.IsAny<long>());
        
        Assert.IsInstanceOf<BadRequestObjectResult>(result);
        var notFoundResult = result as BadRequestObjectResult;
        Assert.That(notFoundResult!.Value, Is.EqualTo("Event not found."));
    }
    
    [Test]
    public async Task LeaveEvent_FailedToLeave_ReturnsBadRequestUserNotFound()
    {
        var leaveEventResult = LeaveEventResult.UserNotFound();

        _eventService.Setup(e => e.LeaveEvent(It.IsAny<long>(), It.IsAny<string>()))
            .ReturnsAsync(leaveEventResult);

        var result = await _controller.LeaveEvent(It.IsAny<long>());
        
        Assert.IsInstanceOf<BadRequestObjectResult>(result);
        var errorResult = result as BadRequestObjectResult;
        Assert.That(errorResult!.Value, Is.EqualTo("Couldn't find user."));
    }
    
    
    [Test]
    public async Task LeaveEvent_ExceptionThrown_ReturnsInternalServerError()
    {

        _eventService.Setup(e => e.LeaveEvent(It.IsAny<long>(), It.IsAny<string>()))
            .ThrowsAsync(new Exception("Something went wrong"));

        var result = await _controller.LeaveEvent(It.IsAny<long>());
        
        Assert.IsInstanceOf<ObjectResult>(result);
        var serverErrorResult = result as ObjectResult;
        Assert.That((serverErrorResult!.Value as Response)!.Message, Is.EqualTo("An error occured on the server."));
    }
    
    //search event method //
    
    
   
    // public async Task SearchEvent_ValidInput_ReturnsOkWithEvents()
    // {
    //     var expectedEvents = new List<Event>
    //     {
    //         new Event
    //         {
    //             EventName = "Event",
    //             //Location = "Location",
    //            //Category = Category,
    //             CategoryId = 123,
    //             Creator = new User(),
    //             CreatorId = "id",
    //             Description = "Description",
    //             StartingDate = DateTime.Today,
    //             EndingDate = DateTime.Today,
    //             HeadCount = 10,
    //             Price = 100,
    //         }
    //     };
    //     
    //     _eventService.Setup(e => e.SearchEventAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<double>(),
    //         It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<double>(), It.IsAny<double>(),
    //         It.IsAny<string>(), It.IsAny<string>()))
    //         .ReturnsAsync(expectedEvents);
    //
    //     var result = await _controller.SearchEvent("EventName", "Location", 10.0, "Category", "StartDate", "EndDate", 0, 100, "SearchType");
    //     
    //     var okObjectResult = Assert.IsType<OkObjectResult>(result);
    //     var response = Assert.IsType<EventsPreviewResponseDto>(okObjectResult.Value);
    //
    //     Assert.Equal(expectedEvents, response.Events);
    // }
}


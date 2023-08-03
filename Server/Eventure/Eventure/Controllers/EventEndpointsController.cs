using Eventure.Models.Entities;
using Eventure.Models.Enums;
using Eventure.Models.RequestDto;
using Eventure.Models.ResponseDto;
using Eventure.Models.Results;
using Eventure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace Eventure.Controllers;

[ApiController]
[Route("[controller]")]
public class EventEndpointsController: ControllerBase
{
    private readonly IEventService _eventService;

    public EventEndpointsController(IEventService eventService)
    {
        _eventService = eventService;
    }

    [Authorize(Roles = "User")]
    [HttpPost]
    public async Task<ActionResult> CreateEvent(CreateEventDto createEventDto)
    {
        try
        {
            var userName = HttpContext.User.Identity!.Name;
            var createEventResult = await _eventService.CreateEventAsync(createEventDto, userName!);
            if (createEventResult.Succeeded)
            {
                return Ok(createEventResult.Response);
            }
            else
            {
                return BadRequest(new { message = "Bad bat!" });
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    [Authorize(Roles = "User")]
    [HttpPost("join-event/{eventId}")]
    public async Task<ActionResult> JoinEvent(long eventId)
    {
        try
        {
            var userName = HttpContext.User.Identity!.Name;
            var joinEventResult = await _eventService.JoinEvent(eventId, userName!);
            
            if (!joinEventResult.Succeeded && joinEventResult.Error != ErrorType.Server)
            {
                return BadRequest();
            }

            if (!joinEventResult.Succeeded)
            {
                return StatusCode(500, "An error occured on the server.");
            }
            
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, "An error occured on the server.");
        }
    }

    [Authorize]
    [HttpPut()]
    public async Task<IActionResult> UpdateEvent(UpdateEventDto updateEventDto)
    {
        try
        {
            var result = await _eventService.UpdateEvent(updateEventDto);
            if (result.Succeeded)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    [Authorize(Roles = "User")]
    [HttpDelete("delete-event/{eventId}")]
    public async Task<IActionResult> DeleteEvent(long eventId)
    {
        try
        {
            var userName = HttpContext.User.Identity!.Name;
            
            if (eventId <= 0)
            {
                return BadRequest("Invalid eventId.");
            }
            
            var deleteEventResult = await _eventService.DeleteEvent(eventId, userName!);
            
            if (!deleteEventResult.Succeeded)
            {
                if (deleteEventResult.Error == ErrorType.EventNotFound)
                {
                    return NotFound("Event not found.");
                }
                else if (deleteEventResult.Error == ErrorType.UserNotFound)
                {
                    return NotFound("User not found.");
                }
                else
                {
                    return StatusCode(500, "An error occurred on the server.");
                }
            }

            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, "An error occured on the server.");
        }   
    }

    [Authorize]
    [HttpPatch("leave-event/{id}")]
    public async Task<IActionResult> LeaveEvent(long id)
    {
        try
        {
            var userName = HttpContext.User.Identity!.Name;
            var result = await _eventService.LeaveEvent(id, userName!);
            if (result.Succeeded)
            {
                return Ok(result.Message);
            }

            return BadRequest(result.Message);
        }
        catch (Exception e)
        {
            var result = new Response() { Message = "Server error" };
            return StatusCode(500, result);
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetEventById(long id)
    {
        try
        {
            var result = await _eventService.GetEventByIdAsync(id);
            if (result.Succeeded)
            {
                return Ok(result.Response);
                
            }

            return BadRequest(result);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            var result = new Response() { Message = "Server error"};
            return StatusCode(500, result);
        }   
    }

    [HttpGet("random")]
    public async Task<IActionResult> GetRandomEvents()
    {
        try
        {
            var randomResult = await _eventService.GetRandomEvent();
            if (randomResult.Succeeded)
            {
                return Ok(randomResult.Response);
            }

            return BadRequest(randomResult);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            var result = new Response() { Message = "Server error"};
            return StatusCode(500, result);
        };
    }

    [HttpGet]
    public async Task<ActionResult<List<Event>>> GetEvents()
    {
        var events = await _eventService.GetEventsAsync();
        var response = new EventsPreviewResponseDto { Events = events };
        return Ok(response);
    }

    [HttpGet("search")]
    public async Task<ActionResult<List<Event>>> SearchEvent(
        string? eventName, 
        string? location, 
        double? distance,
        string? category, 
        string? startingDate, 
        string? endingDate, 
        double? minPrice, 
        double? maxPrice,
        string searchType)
    {
        var userName = HttpContext.User.Identity!.Name;
        var events = await _eventService.SearchEventAsync(
            eventName, 
            location, 
            distance,
            category, 
            startingDate, 
            endingDate, 
            minPrice, 
            maxPrice,
            searchType,
            userName);
        
        var response = new EventsPreviewResponseDto { Events = events };
        return Ok(response);
    }

    [HttpGet("location")]
    public IActionResult SearchLocation(string location)
    {
        try
        {
            var locations = _eventService.GetLocation(location);
            var result = new LocationResponseDto
            {
                Data = locations
            };
            return Ok(result);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            var result = new Response() { Message = "Error in searching location"};
            return StatusCode(500, result);
            
        }
    }

    [HttpGet("category")]
    public IActionResult SearchCategory(string category)
    {
        try
        {
            var categories = _eventService.GetCategory(category);
            var result = new CategoryResponseDto()
            {
                Data = categories
            };
            return Ok(result);
        }
        catch (Exception e)
        {
            Console.WriteLine(e); 
            var result = new Response() { Message = "Error in searching categories"};
            return StatusCode(500, result);
        }
    }
    
}
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
                return BadRequest(new Response(){ Message = "Could not create event" });
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
                return BadRequest(new Response(){Message = "Could not join event."});
            }

            if (!joinEventResult.Succeeded)
            {
                return StatusCode(500, new Response(){Message = "An error occured on the server."});
            }
            
            return Ok( new Response { Message = "Joined event."});
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, new Response(){Message = "An error occured on the server."});
        }
    }

    [Authorize]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateEvent(UpdateEventDto updateEventDto, long id)
    {
        try
        {
            var userName = HttpContext.User.Identity!.Name;
            var result = await _eventService.UpdateEvent(updateEventDto, id, userName);
            if (result.Succeeded)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, new Response(){Message = "An error occured on the server."});
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
                    return NotFound( new Response { Message = "Event not found."});
                }
                if (deleteEventResult.Error == ErrorType.UserNotFound)
                {
                    return NotFound(new Response { Message = "User not found."});
                }
              
                return StatusCode(500, new Response { Message = "An error occurred on the server." });
                
            }

            return Ok( new Response { Message = "Event deleted."});
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, new Response { Message = "An error occured on the server." });
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
            Console.WriteLine(e);
            var result = new Response() { Message = "An error occured on the server." };
            return StatusCode(500, result);
        }
    }
    
    [HttpGet("search")]
    public async Task<IActionResult> SearchEvent(
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
        try
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
        catch (Exception e)
        {
            Console.WriteLine(e);
            var result = new Response() { Message = "An error occured on the server." };
            return StatusCode(500, result);
        }
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
            var result = new Response() { Message = "An error occured on the server."};
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
            var result = new Response() { Message = "An error occured on the server."};
            return StatusCode(500, result);
        }
    }
    
}
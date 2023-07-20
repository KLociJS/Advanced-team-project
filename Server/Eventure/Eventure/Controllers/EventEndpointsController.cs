using Eventure.Models.RequestDto;
using Eventure.Models.ResponseDto;
using Eventure.Services;
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

    [HttpPost()]
    public async Task<ActionResult> CreateEvent(CreateEventDto createEventDto)
    {
        try
        {
            var createEventResult = await _eventService.CreateEventAsync(createEventDto);
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

    [HttpPut()]
    public async Task<IActionResult> UpdateEvent(string id)
    {
        try
        {
            var result = await _eventService.UpdateEvent(id);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        ;
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEvent(string id)
    {
        try
        {
            var result = await _eventService.DeleteEvent(id);
            if (result.Succeeded)
            {
                return Ok(result.Response);
                
            }

            return BadRequest(result.Response);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            var result = new Response() { Message = "Server error"};
            return StatusCode(500, result);
        }   
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetEventById(string id)
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

    [HttpGet("search")]
    public IActionResult SearchEvent()
    {
        return Ok();
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
using Eventure.Models.RequestDto;
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
                return BadRequest(new{message = "Bad bat!"} );
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    [HttpPut()]
    public IActionResult UpdateEvent()
    {
        return Ok();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteEvent(int id)
    {
        return Ok(id);
    }

    [HttpGet("{id}")]
    public IActionResult GetEventById(int id)
    {
        return Ok(id);
    }

    [HttpGet("random")]
    public IActionResult GetRandomEvents()
    {
        return Ok();
    }

    [HttpGet("search")]
    public IActionResult SearchEvent()
    {
        return Ok();
    }
}
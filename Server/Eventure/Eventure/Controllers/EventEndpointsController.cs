using Microsoft.AspNetCore.Mvc;
namespace Eventure.Controllers;

[ApiController]
[Route("[controller]")]
public class EventEndpointsController: ControllerBase
{
    [HttpPost()]
    public IActionResult CreateEvent()
    {
        return Ok();
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
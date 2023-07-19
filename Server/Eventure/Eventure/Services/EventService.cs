using Eventure.Models;
using Eventure.Models.Entities;

namespace Eventure.Services;

public class EventService : IEventService
{
    private readonly EventureContext _context;

    public EventService(EventureContext eventureContext)
    {
        _context = eventureContext;
    }
    public async Task CreateEvent(Event newEvent)
    { 
        _context.Events.Add(newEvent);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteEvent(long id)
    {
        var eventToDelete = _context.Events.FindAsync(id).Result;
        if (eventToDelete != null)
        {
            _context.Events.Remove(eventToDelete);
            await _context.SaveChangesAsync();
        }
        else
        {
            throw new Exception("There is no event to delete");
        }
    }

    public async Task UpdateEvent(Event eventToUpdate)
    {
        var eventFound = _context.Events.FindAsync(eventToUpdate.Id).Result;
        if (eventFound != null)
        {
            eventFound.Location = eventToUpdate.Location;
            eventFound.Date = eventToUpdate.Date;
            eventFound.Duration = eventToUpdate.Duration;
            eventFound.Category = eventToUpdate.Category;
            
            
            _context.Events.Update(eventFound);
            await _context.SaveChangesAsync();
        }
    }
}
using Eventure.Models;
using Eventure.Models.Entities;
using Eventure.Models.RequestDto;
using Eventure.Models.Results;

namespace Eventure.Services;

public class EventService : IEventService
{
    private readonly EventureContext _context;

    public EventService(EventureContext eventureContext)
    {
        _context = eventureContext;
    }
    public async Task<EventActionResult> CreateEventAsync(CreateEventDto createEventDto)
    {
        try
        {
            var newEvent = new Event()
            {
                Date = createEventDto.Date,
                Visibility = createEventDto.Visibility,
                HeadCount = createEventDto.HeadCount,
                RecommendedAge = createEventDto.RecommendedAge,
                Duration = createEventDto.Duration,
                Price = createEventDto.Price,
                LocationId = createEventDto.LocationId,
                CategoryId = createEventDto.CategoryId,
                UserId = createEventDto.UserId
            };
            
        await _context.SaveChangesAsync();
        return EventActionResult.Succeed("Event created");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
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

    public List<Location> GetLocation(string location)
    {
        return _context.Locations.Where(l => l.Name.StartsWith(location)).ToList();
    }

    public List<Category> GetCategory(string category)
    {
        return _context.Categories.Where(c => c.Name.Equals(category)).ToList();
    }
}
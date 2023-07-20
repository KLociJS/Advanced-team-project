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
            var utcTimeZone = TimeZoneInfo.Utc;
            var newEvent = new Event()
            {
                EventName = createEventDto.EventName,
                StartingDate = DateTimeOffset.ParseExact(createEventDto.StartingDate, "yyyy-MM-dd", null).DateTime,
                EndingDate = DateTimeOffset.ParseExact(createEventDto.EndingDate, "yyyy-MM-dd", null).DateTime,
                HeadCount = createEventDto.HeadCount,
                RecommendedAge = createEventDto.RecommendedAge,
                Price = createEventDto.Price,
                LocationId = createEventDto.LocationId,
                CategoryId = createEventDto.CategoryId,
                UserId = createEventDto.UserId
            };
            
            newEvent.StartingDate = TimeZoneInfo.ConvertTimeToUtc(newEvent.StartingDate, utcTimeZone);
            newEvent.EndingDate = TimeZoneInfo.ConvertTimeToUtc(newEvent.EndingDate, utcTimeZone);

            await _context.Events.AddAsync(newEvent);
            
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
            eventFound.Category = eventToUpdate.Category;
            
            
            _context.Events.Update(eventFound);
            await _context.SaveChangesAsync();
        }
    }

    public List<Location> GetLocation(string location)
    {
        var formattedLocation = location.Substring(0, 1).ToUpper() + location.Substring(1);
        return _context.Locations.Where(l => l.Name.Contains(formattedLocation)).ToList();
    }

    public List<Category> GetCategory(string category)
    {
        var formattedCategory = category.Substring(0, 1).ToUpper() + category.Substring(1);
        return _context.Categories.Where(c => c.Name.Contains(formattedCategory)).ToList();
    }
}
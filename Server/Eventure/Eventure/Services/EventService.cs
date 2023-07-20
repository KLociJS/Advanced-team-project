using Eventure.Models;
using Eventure.Models.Entities;
using Eventure.Models.RequestDto;
using Eventure.Models.Results;
using Microsoft.EntityFrameworkCore;

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
    public async Task<GetEventResult> GetEventByIdAsync(string id)
    {
        try
        {
            var eventById = _context.Events.FindAsync(id).Result;
            if (eventById != null)
            {
                return GetEventResult.Succeed("Event found", eventById);
            }
            return GetEventResult.Failed("Event not found");
            
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        };
    }

    public async Task<GetEventResult> GetRandomEvent()
    {
        try
        {
            var allEvents = _context.Events.ToListAsync().Result;
            if (allEvents.Count == 0)
            {
                return GetEventResult.Failed("No event found");
            }

            Random random = new Random();
            int randomIndex = random.Next(0, allEvents.Count);
            var randomEvent = allEvents[randomIndex];
            return GetEventResult.Succeed("Random event found", randomEvent);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        ;
    }

    public async Task<EventActionResult> DeleteEvent(string id)
    {
        try
        {
            var eventToDelete = _context.Events.FindAsync(id).Result;
            if (eventToDelete != null)
            {
                _context.Events.Remove(eventToDelete);
                await _context.SaveChangesAsync();
                return EventActionResult.Succeed("Event deleted successfully");
            }

            return EventActionResult.Failed("Couldn't find event by id");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<GetEventResult> UpdateEvent(Event eventToUpdate)
    {
        try
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
                return GetEventResult.Succeed("Event updated successfully", eventFound);
            }

            return GetEventResult.Failed("Couldn't find event by id");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
    }

    public List<Location> GetLocation(string location)
    {
        location = location.Substring(0, 1).ToUpper() + location.Substring(1);
        return _context.Locations.Where(l => l.Name.StartsWith(location)).ToList();
    }

    public List<Category> GetCategory(string category)
    {
        category = category.Substring(0, 1).ToUpper() + category.Substring(1);
        return _context.Categories.Where(c => c.Name.Equals(category)).ToList();
    }

    
}
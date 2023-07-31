using Eventure.Models;
using Eventure.Models.Entities;
using Eventure.Models.RequestDto;
using Eventure.Models.ResponseDto;
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
            var location = await _context.Locations.FirstOrDefaultAsync(l => l.Name.ToLower() == createEventDto.Location.ToLower());
            if (location == null)
            {
                return EventActionResult.Failed("Couldn't find location.");
            }
            
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Name.ToLower() == createEventDto.Category.ToLower());
            if (category == null)
            {
                return EventActionResult.Failed("Couldn't find category.");
            }
            
            var newEvent = new Event()
            {
                EventName = createEventDto.EventName,
                Description = createEventDto.Description,
                StartingDate = Convert.ToDateTime(createEventDto.StartingDate).ToUniversalTime(),
                EndingDate = Convert.ToDateTime(createEventDto.EndingDate).ToUniversalTime(),
                HeadCount = createEventDto.HeadCount,
                RecommendedAge = createEventDto.RecommendedAge,
                Price = createEventDto.Price,
                Location = location,
                Category = category,
                UserId = createEventDto.UserId
            };

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
    public async Task<GetEventResult> GetEventByIdAsync(long id)
    {
        try
        {
            var eventById = await _context.Events.FindAsync(id);
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
        }
    }

    public async Task<List<Event>> GetEventsAsync()
    {
        try
        {
            var events = _context.Events
                .Include(e => e.Location)
                .Include(e => e.Category);
            return await events.ToListAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<List<Event>> SearchEventAsync(
        string? eventName, 
        string? location,
        double? distance,
        string? category, 
        string? startingDate, 
        string? endingDate, 
        double? minPrice, 
        double? maxPrice)
    {
        try
        {
            IQueryable<Event> events = _context.Events
                .Include(e => e.Location)
                .Include(e => e.Category);
            
            if (eventName != null)
            {
                events = events.Where(e => e.EventName.ToLower().Contains(eventName.ToLower()));
            }

            if (location != null && distance == null)
            {
                events = events.Where(e => e.Location!.Name.ToLower().Contains(location.ToLower()));
            }
            
            if (location != null && distance != null)
            {
                var originLocation = await _context.Locations.FirstOrDefaultAsync(l =>l.Name == location );
                var allLocations = await _context.Locations.ToListAsync();
                var goodLocations = allLocations.Where(l => CalculateDistance(originLocation, l)<= distance);
                events = events.Where(e => goodLocations.Contains(e.Location));
            }

            if (category != null)
            {
                events = events.Where(e => e.Category!.Name.ToLower().Contains(category.ToLower()));
            }

            if (minPrice != null)
            {
                events = events.Where(e => e.Price > minPrice);
            }
            
            if (maxPrice != null)
            {
                events = events.Where(e => e.Price < maxPrice);
            }

            if (startingDate != null)
            {
                var startingDateUtc = Convert.ToDateTime(startingDate).ToUniversalTime();
                events = events.Where(e => e.StartingDate > startingDateUtc);
            }
            
            if (endingDate != null)
            {
                var endingDateUtc = Convert.ToDateTime(endingDate).ToUniversalTime();
                events = events.Where(e => e.EndingDate < endingDateUtc);
            }
            
            return await events.ToListAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<GetEventResult> GetRandomEvent()
    {
        try
        {
            var allEvents = await _context.Events.ToListAsync();
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
    }

    public async Task<EventActionResult> DeleteEvent(long id)
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

    public async Task<UpdateEventResult> UpdateEvent(UpdateEventDto updateEventDto)
    {
        try
        {
            var eventFound = await _context.Events
                .Include(e => e.Location) // Include the Location
                .Include(e => e.Category) // Include the Category
                .FirstOrDefaultAsync(e => e.Id == updateEventDto.Id);
            if (eventFound != null)
            {
                eventFound.LocationId = updateEventDto.LocationId;
                eventFound.CategoryId = updateEventDto.CategoryId;
                eventFound.EventName = updateEventDto.EventName;
                eventFound.Description = updateEventDto.Description;
                eventFound.StartingDate = updateEventDto.StartingDate;
                eventFound.EndingDate = updateEventDto.EndingDate;
                eventFound.HeadCount = updateEventDto.HeadCount;
                eventFound.RecommendedAge = updateEventDto.RecommendedAge;
                eventFound.Price = updateEventDto.Price;
                
                _context.Events.Update(eventFound);
                await _context.SaveChangesAsync();
                
                var resultEvent = new EventPreviewResponseDto()
                {
                    Id = updateEventDto.Id,
                    EventName = updateEventDto.EventName,
                    Description = updateEventDto.Description,
                    StartingDate = updateEventDto.StartingDate,
                    EndingDate = updateEventDto.EndingDate,
                    HeadCount = updateEventDto.HeadCount,
                    RecommendedAge = updateEventDto.RecommendedAge,
                    Price = updateEventDto.Price,
                    Location = eventFound.Location,
                    Category = eventFound.Category
                };
                
                return UpdateEventResult.Success(resultEvent);
            }

            return UpdateEventResult.Fail();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
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
    
    
    
        private const double EarthRadiusKm = 6371.0;
        private static double DegreesToRadians(double degrees)
        {
            return degrees * Math.PI / 180.0;
        }
        public static double CalculateDistance(Location originLocation, Location goodLocation)
        {
            double lat1 = originLocation.Latitude;
            double lat2 = goodLocation.Latitude;
            double lon1 = originLocation.Longitude;
            double lon2 = goodLocation.Longitude;
            
            double dLat = DegreesToRadians(lat2 - lat1);
            double dLon = DegreesToRadians(lon2 - lon1);

            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                       Math.Cos(DegreesToRadians(lat1)) * Math.Cos(DegreesToRadians(lat2)) *
                       Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            double distance = EarthRadiusKm * c;
            return distance;
        }
   
}
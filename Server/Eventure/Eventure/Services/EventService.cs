using Eventure.Models;
using Eventure.Models.Entities;
using Eventure.Models.RequestDto;
using Eventure.Models.ResponseDto;
using Eventure.Models.Results;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Eventure.Services;

public class EventService : IEventService
{
    private readonly EventureContext _context;
    private readonly UserManager<User> _userManager;

    public EventService(EventureContext eventureContext, UserManager<User> userManager)
    {
        _context = eventureContext;
        _userManager = userManager;
    }
    public async Task<EventActionResult> CreateEventAsync(CreateEventDto createEventDto, string userName)
    {
        try
        {
            var user = await _userManager.FindByNameAsync(userName);
            
            
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
                Creator = user
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

    public async Task<JoinEventResult> JoinEvent(long eventId, string userName)
    {
        try
        {
            var eventToFind = await _context.Events.FindAsync(eventId);
            if (eventToFind == null)
            {
                return JoinEventResult.EventNotFound();
            }

            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
            {
                return JoinEventResult.UserNotFound();
            }
            
            eventToFind.Participants.Add(user);

            var joinEventResult = await _context.SaveChangesAsync();

            if (joinEventResult > 0)
            {
                return JoinEventResult.Success();
            }
            return JoinEventResult.ServerError();
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
            var eventById = await _context.Events
                .Include(e=>e.Category)
                .Include(e=>e.Location)
                .FirstOrDefaultAsync(e=>e.Id==id);
            
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
        double? maxPrice,
        string searchType,
        string? userName)
    {
        try
        {
            IQueryable<Event> events = null!;
            if (searchType == "all")
            {
                if (userName != null)
                {
                    var user = await _userManager.FindByNameAsync(userName);
                    events = _context.Events
                        .Include(e => e.Location)
                        .Include(e => e.Category)
                        .Include(e => e.Participants)
                        .Where(e=>!e.Participants.Contains(user));
                }
                else
                {
                    events = _context.Events
                        .Include(e => e.Location)
                        .Include(e => e.Category);
                }

            }
            else if(searchType == "applied")
            {
                var user = await _userManager.FindByNameAsync(userName);
                events = _context.Events
                    .Include(e => e.Location)
                    .Include(e => e.Category)
                    .Include(e => e.Participants)
                    .Where(e => e.Participants.Contains(user));
            }
            else if (searchType == "created")
            {
                var user = await _userManager.FindByNameAsync(userName);
                events = _context.Events
                    .Include(e => e.Location)
                    .Include(e => e.Category)
                    .Where(e => e.Creator == user);
            }
            
            
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

    public async Task<DeleteEventResult> DeleteEvent(long eventId, string userName)
    {
        try
        {
            if (eventId <= 0 || string.IsNullOrEmpty(userName))
            {
                return DeleteEventResult.ServerError(); 
            }

            var user = await _userManager.FindByNameAsync(userName);
            var eventToDelete = await _context.Events
                .Include(e => e.Creator)
                .FirstOrDefaultAsync(e =>  e.Id == eventId);
            if (eventToDelete != null && eventToDelete.Creator == user)
            {
                _context.Events.Remove(eventToDelete);
                await _context.SaveChangesAsync();
                return DeleteEventResult.Success();
            }
            return DeleteEventResult.EventNotFound();
            
            
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<LeaveEventResult> LeaveEvent(long id, string username)
    {
        try
        {
            var eventToLeave = await _context.Events
                .Include(e=> e.Participants)
                .FirstOrDefaultAsync(e => e.Id == id);
            var userToLeave = await _context.Users
                .FirstOrDefaultAsync(u => u.UserName == username);

            if (eventToLeave == null)
            {
                return LeaveEventResult.EventNotFound();
            }

            if (userToLeave == null)
            {
                return LeaveEventResult.UserNotFound();
            }

            if (!eventToLeave.Participants.Contains(userToLeave))
            {
                return LeaveEventResult.UserIsNotParticipant();
            }

            eventToLeave.Participants.Remove(userToLeave);
            await _context.SaveChangesAsync();
            return LeaveEventResult.Success();
        }
        catch
        {
            return LeaveEventResult.ServerError();
        }
    }


    public async Task<UpdateEventResult> UpdateEvent(UpdateEventDto updateEventDto, long id, string userName)
    {
        try
        {
            var user = await _userManager.FindByNameAsync(userName);
            var eventFound = await _context.Events
                .Include(e => e.Location) 
                .Include(e => e.Category) 
                .Include(e=>e.Creator)
                .FirstOrDefaultAsync(e => e.Id == id);
            
            var location = await _context.Locations.FirstOrDefaultAsync(l => l.Name.ToLower() == updateEventDto.Location.ToLower());
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Name.ToLower() == updateEventDto.Category.ToLower());

            if (eventFound != null && eventFound.Creator==user)
            {
                eventFound.EventName = updateEventDto.EventName;
                eventFound.Description = updateEventDto.Description;
                eventFound.StartingDate = Convert.ToDateTime(updateEventDto.StartingDate).ToUniversalTime();
                eventFound.EndingDate = Convert.ToDateTime(updateEventDto.EndingDate).ToUniversalTime();
                eventFound.HeadCount = updateEventDto.HeadCount;
                eventFound.RecommendedAge = updateEventDto.RecommendedAge;
                eventFound.Price = updateEventDto.Price;
                eventFound.Location = location;
                eventFound.Category = category;
                
                _context.Events.Update(eventFound);
                await _context.SaveChangesAsync();
                
                var resultEvent = new EventPreviewResponseDto()
                {
                    EventName = updateEventDto.EventName,
                    Description = updateEventDto.Description,
                    StartingDate = eventFound.StartingDate,
                    EndingDate = eventFound.EndingDate,
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

        double distance = 6371.0 * c;
        return distance;
    }
   
}
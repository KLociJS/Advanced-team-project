using Eventure.Models.Entities;
using Eventure.Models.RequestDto;
using Eventure.Models.Results;

namespace Eventure.Services;

public interface IEventService
{
    Task<EventActionResult> CreateEventAsync(CreateEventDto createEventDto, string userName);
    Task<JoinEventResult> JoinEvent(long eventId, string userName);
    Task<EventActionResult> DeleteEvent(long id);
    Task<UpdateEventResult>  UpdateEvent(UpdateEventDto updateEventDto);
    Task<List<Event>> SearchEventAsync(string? eventName, 
        string? location, 
        double? distance,
        string? category, 
        string? startingDate, 
        string? endingDate, 
        double? minPrice, 
        double? maxPrice,
        string searchType,
        string? userName);
    Task<List<Event>> GetEventsAsync();
    List<Location> GetLocation(string location);
    List<Category> GetCategory(string category);
    Task<GetEventResult> GetEventByIdAsync(long id);
    Task<GetEventResult> GetRandomEvent();
    
}
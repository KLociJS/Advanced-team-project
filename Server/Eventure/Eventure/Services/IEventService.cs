using Eventure.Models.Entities;
using Eventure.Models.RequestDto;
using Eventure.Models.Results;

namespace Eventure.Services;

public interface IEventService
{
    Task<EventActionResult> CreateEventAsync(CreateEventDto createEventDto);
    Task<EventActionResult> DeleteEvent(string id);
    Task<GetEventResult>  UpdateEvent(Event eventToUpdate);
    List<Location> GetLocation(string location);
    List<Category> GetCategory(string category);
    Task<GetEventResult> GetEventByIdAsync(string id);
    Task<GetEventResult> GetRandomEvent();
}
using Eventure.Models.Entities;
using Eventure.Models.RequestDto;
using Eventure.Models.Results;

namespace Eventure.Services;

public interface IEventService
{
    Task<EventActionResult> CreateEventAsync(CreateEventDto createEventDto);
    Task DeleteEvent(long id);
    Task UpdateEvent(Event eventToUpdate);
}
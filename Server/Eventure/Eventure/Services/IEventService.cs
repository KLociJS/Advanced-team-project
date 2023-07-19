using Eventure.Models.Entities;

namespace Eventure.Services;

public interface IEventService
{
    Task CreateEvent(Event newEvent);
    Task DeleteEvent(long id);
    Task UpdateEvent(Event eventToUpdate);
}
﻿using Eventure.Models.Entities;
using Eventure.Models.RequestDto;
using Eventure.Models.Results;

namespace Eventure.Services;

public interface IEventService
{
    Task<EventActionResult> CreateEventAsync(CreateEventDto createEventDto, string userName);
    Task<JoinEventResult> JoinEvent(long eventId, string userName);
    Task<LeaveEventResult> LeaveEvent(long id, string username);
    Task<DeleteEventResult> DeleteEvent(long eventId, string userName);
    Task<UpdateEventResult>  UpdateEvent(UpdateEventDto updateEventDto, long id, string userName);
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

    List<Location> GetLocation(string location);
    List<Category> GetCategory(string category);

    
}
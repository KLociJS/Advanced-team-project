﻿using Eventure.Models.Entities;
using Eventure.Models.RequestDto;
using Eventure.Models.Results;

namespace Eventure.Services;

public interface IEventService
{
    Task<EventActionResult> CreateEventAsync(CreateEventDto createEventDto);
    Task<EventActionResult> DeleteEvent(long id);
    Task<UpdateEventResult>  UpdateEvent(UpdateEventDto updateEventDto);
    Task<List<Event>> SearchEventAsync();
    List<Location> GetLocation(string location);
    List<Category> GetCategory(string category);
    Task<GetEventResult> GetEventByIdAsync(string id);
    Task<GetEventResult> GetRandomEvent();
}
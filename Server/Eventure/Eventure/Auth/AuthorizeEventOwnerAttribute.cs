using System.Security.Claims;
using Eventure.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Eventure.Auth;

public class AuthorizeEventOwnerAttribute : ActionFilterAttribute
{
    private readonly IEventService _eventService;

    public AuthorizeEventOwnerAttribute(IEventService eventService)
    {
        _eventService = eventService;
    }

    public override async void OnActionExecuting(ActionExecutingContext context)
    {
        if (context.RouteData.Values.TryGetValue("eventId", out var eventIdObj) && eventIdObj is long eventId)
        {
            var authenticatedUserId = context.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var eventToFind = await _eventService.GetEventByIdAsync(eventId);

            if (eventToFind.EventData!.CreatorId != authenticatedUserId)
            {
                context.Result = new ForbidResult();
                return;
            }
        }
    }
}
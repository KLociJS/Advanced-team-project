using Eventure.Models.Entities;
using Eventure.Models.ResponseDto;

namespace Eventure.Models.Results;

public class GetEventResult
{
    public bool Succeeded { get; set; }
    public Response? Response { get; set; }
    public Event? EventData { get; set; } 

    public static GetEventResult Succeed(string message, Event eventData)
    {
        var response = new Response() { Message = message };
        return new GetEventResult() { Succeeded = true, EventData = eventData, Response = response};
    }

    public static GetEventResult Failed(string message)
    {
        var response = new Response() { Message = message };
        return new GetEventResult() { Succeeded = false, Response = response};

    }
}
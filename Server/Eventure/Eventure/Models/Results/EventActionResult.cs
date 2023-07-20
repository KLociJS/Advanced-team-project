using Eventure.Models.ResponseDto;

namespace Eventure.Models.Results;

public class EventActionResult
{
    public bool Succeeded { get; set; }
    public Response Response { get; set; }

    public static EventActionResult Succeed(string message)
    {
        var response = new Response() { Message = message };
        return new EventActionResult() { Succeeded = true, Response = response};
    }

    public static EventActionResult Failed(string message)
    {
        var response = new Response() { Message = message };
        return new EventActionResult() { Succeeded = false, Response = response};

    }
}
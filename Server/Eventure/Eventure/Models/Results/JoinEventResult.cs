using Eventure.Models.Enums;

namespace Eventure.Models.Results;

public class JoinEventResult
{
    public bool Succeeded { get; set; }
    public ErrorType? Error { get; set; }
    public string Message { get; set; } = string.Empty;

    public JoinEventResult(bool isSuccessful)
    {
        Succeeded = isSuccessful;
    }

    public static JoinEventResult EventNotFound()
    {
        var result = new JoinEventResult(false)
        {
            Error = ErrorType.EventNotFound,
            Message = "Event not found."
        };

        return result;
    }

    public static JoinEventResult UserNotFound()
    {
        var result = new JoinEventResult(false)
        {
            Error = ErrorType.UserNotFound,
            Message = "Couldn't find user."
        };
        return result;
    }

    public static JoinEventResult Success()
    {
        var result = new JoinEventResult(true)
        {
            Message = "Joined event."
        };
        return result;
    }

    public static JoinEventResult ServerError()
    {
        var result = new JoinEventResult(false)
        {
            Error = ErrorType.Server,
            Message = "Couldn't join event due to server error."
        };
        return result;
    }
}
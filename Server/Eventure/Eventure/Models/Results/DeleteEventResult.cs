using Eventure.Models.Enums;

namespace Eventure.Models.Results;

public class DeleteEventResult
{
    public bool Succeeded { get; set; }
    public ErrorType? Error { get; set; }
    public string Message { get; set; } = string.Empty;

    public DeleteEventResult(bool isSuccessful)
    {
        Succeeded = isSuccessful;
    }

    public static DeleteEventResult EventNotFound()
    {
        var result = new DeleteEventResult(false)
        {
            Error = ErrorType.EventNotFound,
            Message = "Event not found."
        };

        return result;
    }

    public static DeleteEventResult UserNotFound()
    {
        var result = new DeleteEventResult(false)
        {
            Error = ErrorType.UserNotFound,
            Message = "Couldn't find user."
        };
        return result;
    }

    public static DeleteEventResult Success()
    {
        var result = new DeleteEventResult(true)
        {
            Message = "Deleted event."
        };
        return result;
    }

    public static DeleteEventResult ServerError()
    {
        var result = new DeleteEventResult(false)
        {
            Error = ErrorType.Server,
            Message = "Couldn't delete event due to server error."
        };
        return result;
    }
}

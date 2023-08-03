using Eventure.Models.Enums;

namespace Eventure.Models.Results;

public class LeaveEventResult
{
    public bool Succeeded { get; set; }
    public ErrorType? Error { get; set; }
    public string Message { get; set; } = string.Empty;

    public LeaveEventResult(bool isSuccessful)
    {
        Succeeded = isSuccessful;
    }

    public static LeaveEventResult EventNotFound()
    {
        var result = new LeaveEventResult(false)
        {
            Error = ErrorType.EventNotFound,
            Message = "Event not found."
        };

        return result;
    }

    public static LeaveEventResult UserNotFound()
    {
        var result = new LeaveEventResult(false)
        {
            Error = ErrorType.UserNotFound,
            Message = "Couldn't find user."
        };
        return result;
    }

    public static LeaveEventResult UserIsNotParticipant()
    {
        var result = new LeaveEventResult(false)
        {
            Error = ErrorType.UserNotFound,
            Message = "User was not participant of the event"
        };
        return result;
    }

    public static LeaveEventResult Success()
    {
        var result = new LeaveEventResult(true)
        {
            Message = "User left the event."
        };
        return result;
    }

    public static LeaveEventResult ServerError()
    {
        var result = new LeaveEventResult(false)
        {
            Error = ErrorType.Server,
            Message = "Couldn't leave event due to server error."
        };
        return result;
    }
}
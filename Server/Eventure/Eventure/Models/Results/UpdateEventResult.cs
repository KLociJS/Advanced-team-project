using Eventure.Models.ResponseDto;

namespace Eventure.Models.Results;

public class UpdateEventResult
{
    public bool Succeeded { get; private set; }
    
    public string Message { get; set; } = string.Empty;
    public EventPreviewResponseDto? Data { get; set; }

    public static UpdateEventResult Success(EventPreviewResponseDto eventPreviewResponseDto)
    {
        return new UpdateEventResult() { Succeeded = true, Data = eventPreviewResponseDto, Message = "Event successfully updated."};
    }
    public static UpdateEventResult Fail()
    {
        return new UpdateEventResult() { Succeeded = false, Message = "Event with given id doesnt exists."};
    }
}
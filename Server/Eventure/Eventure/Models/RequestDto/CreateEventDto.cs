using Eventure.Models.Enums;

namespace Eventure.Models.RequestDto;

public class CreateEventDto
{
    public string EventName { get; set; } = string.Empty;
    public string StartingDate { get; set; }
    public string EndingDate { get; set; }
    public int HeadCount { get; set; }
    public int RecommendedAge { get; set; }
    public int Price { get; set; }
    public long LocationId { get; set; }
    public long CategoryId { get; set; }
    public string UserId { get; set; } = string.Empty;

}
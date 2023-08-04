namespace Eventure.Models.RequestDto;

public class UpdateEventDto
{
    public string EventName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string StartingDate { get; set; } = string.Empty;
    public string EndingDate { get; set; } = string.Empty;
    public int HeadCount { get; set; }
    public int RecommendedAge { get; set; }
    public int Price { get; set; }
    public string Location { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
}
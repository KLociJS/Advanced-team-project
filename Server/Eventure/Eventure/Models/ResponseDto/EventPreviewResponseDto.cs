using Eventure.Models.Entities;

namespace Eventure.Models.ResponseDto;

public class EventPreviewResponseDto
{
    public long Id { get; set; }
    public string EventName { get; set; } = string.Empty;
    
    public string Description { get; set; } = string.Empty;
    public DateTime StartingDate { get; set; }
    public DateTime EndingDate { get; set; }
    public int HeadCount { get; set; }
    public int RecommendedAge { get; set; }
    public int Price { get; set; }
    public Location? Location { get; set; }
    public Category? Category { get; set; }
    
    public string UserId { get; set; } = string.Empty;
}
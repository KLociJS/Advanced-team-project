namespace Eventure.Models.RequestDto;

public class UpdateEventDto
{
    public long Id { get; set; }
    public string EventName { get; set; } = string.Empty;
    
    public string Description { get; set; } = string.Empty;
    public DateTime StartingDate { get; set; }
    public DateTime EndingDate { get; set; }
    public int HeadCount { get; set; }
    public int RecommendedAge { get; set; }
    public int Price { get; set; }
    public long LocationId { get; set; }
    public long CategoryId { get; set; }
}
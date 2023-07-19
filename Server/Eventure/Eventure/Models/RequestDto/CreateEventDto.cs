using Eventure.Models.Enums;

namespace Eventure.Models.RequestDto;

public class CreateEventDto
{
    public DateTime Date { get; set; }
    public Visibility Visibility { get; set; }
    public int HeadCount { get; set; }
    public int RecommendedAge { get; set; }
    public int Duration{ get; set; }
    public int Price { get; set; }
    public long LocationId { get; set; }
    public long CategoryId { get; set; }
    public string UserId { get; set; } = string.Empty;

}
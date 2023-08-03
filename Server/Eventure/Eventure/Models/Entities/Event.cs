using System.ComponentModel.DataAnnotations.Schema;
using Eventure.Models.Enums;

namespace Eventure.Models.Entities;

public class Event
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    public string EventName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime StartingDate { get; set; }
    public DateTime EndingDate { get; set; }
    public int HeadCount { get; set; }
    public int RecommendedAge { get; set; }
    public int Price { get; set; }
    public long LocationId { get; set; }
    public Location? Location { get; set; }
    public long CategoryId { get; set; }
    public Category? Category { get; set; }
    public List<User> Participants { get; } = new();
    public string CreatorId { get; set; } = string.Empty;
    public User? Creator { get; set; }
}
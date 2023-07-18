using System.ComponentModel.DataAnnotations.Schema;

namespace Eventure.Models.Entities;

public class Location
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public Double Latitude { get; set; }
    public Double Longitude { get; set; }
    public List<Event> Events { get; set; } = new List<Event>();
}
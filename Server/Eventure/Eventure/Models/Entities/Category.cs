using System.ComponentModel.DataAnnotations.Schema;
using Eventure.Models.Enums;

namespace Eventure.Models.Entities;

public class Category
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    public Categories Name { get; set; }
    
    public List<Event> Events = new List<Event>();
}
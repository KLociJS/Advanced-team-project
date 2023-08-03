using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;

namespace Eventure.Models.Entities;

public class User:IdentityUser
{

    public string Name { get; set; } =string.Empty;
    public static DateTime BirthDate { get; set; }
    public int Age = (int) ((DateTime.Now - BirthDate).TotalDays/365.242199);

    [JsonIgnore]
    public List<Event> AppliedEvents { get; } = new();
    [JsonIgnore]
    public List<Event> CreatedEvents { get; set; } = new List<Event>();


}
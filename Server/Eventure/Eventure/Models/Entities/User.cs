using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Eventure.Models.Entities;

public class User:IdentityUser
{

    public string Name { get; set; } =string.Empty;
    public static DateTime BirthDate { get; set; }
    public int Age = (int) ((DateTime.Now - BirthDate).TotalDays/365.242199);

    public List<Event> AppliedEvents { get; } = new();
    public List<Event> CreatedEvents { get; set; } = new List<Event>();


}
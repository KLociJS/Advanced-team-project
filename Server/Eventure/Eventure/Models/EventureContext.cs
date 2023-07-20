using System.Reflection;
using Eventure.Models.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Eventure.Models;

public class EventureContext : IdentityDbContext<User>
{
    public DbSet<Event>Events { get; set; }
    public DbSet<Location> Locations { get; set; }
    public DbSet<Category>Categories { get; set; }
    public DbSet<AppliedEvent>AppliedEvents { get; set; }


    public EventureContext(DbContextOptions<EventureContext> options) : base(options)
    {
        Seed(this);
    }
  
    public static void Seed(EventureContext context)
    {
        if (!context.Locations.Any())
        {
            var fileName = "hu.csv";
            var folderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var filePath = Path.Combine(folderPath, "Advanced-team-project", fileName);
            var locations = Location.LoadLocationsFromCsv(filePath);
                
            context.Locations.AddRange(locations);
            context.SaveChanges();
        }
    }
}
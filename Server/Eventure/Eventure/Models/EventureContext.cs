using System.Reflection;
using Eventure.Models.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Eventure.Models;

public class EventureContext : IdentityDbContext<User>
{
    public DbSet<Event> Events { get; set; }
    public DbSet<Location> Locations { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<AppliedEvent> AppliedEvents { get; set; }


    public EventureContext(DbContextOptions<EventureContext> options) : base(options)
    {
    }

    public static void Seed(EventureContext context)
    {
        if (!context.Locations.Any())
        {
            var fileName = "hu.csv";
            var locations = Location.LoadLocationsFromCsv(fileName);

            context.Locations.AddRange(locations);
            context.SaveChanges();
        }

        if (!context.Categories.Any())
        {
            var fileName = "Category.csv";

            var categories = Category.LoadCategoriesFromCsv(fileName);
            context.Categories.AddRange(categories);
            context.SaveChanges();
        }
    }
}
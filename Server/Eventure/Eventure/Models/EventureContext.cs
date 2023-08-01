using System.Reflection;
using Eventure.Models.Entities;
using Microsoft.AspNetCore.Identity;
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

    public static async Task Seed(EventureContext context, UserManager<User> userManager)
    {
        if (!context.Locations.Any())
        {
            var fileName = "hu.csv";
            var locations = Location.LoadLocationsFromCsv(fileName);

            context.Locations.AddRange(locations);
            await context.SaveChangesAsync();
        }

        if (!context.Categories.Any())
        {
            var fileName = "Category.csv";

            var categories = Category.LoadCategoriesFromCsv(fileName);
            context.Categories.AddRange(categories);
            await context.SaveChangesAsync();
        }

        var oldUsers = await userManager.Users.ToListAsync();
        
        foreach (var user in oldUsers)
        {
            await userManager.DeleteAsync(user);
        }

        var newUsers = new List<User>()
        {
            new() { UserName = "Loci", Email = "loci@gmail.com" },
            new () { UserName = "Zsofi", Email = "zsofi@gmail.com" },
            new () { UserName = "Bianka", Email = "bianka@gmail.com" }
        };
        
        foreach (var newUser in newUsers)
        {
            await userManager.CreateAsync(newUser, "Abcd@1234");
        }

    }
}
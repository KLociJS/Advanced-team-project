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

    public static async Task Seed(EventureContext context, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
    {
        // Seed locations
        if (!context.Locations.Any())
        {
            var fileName = "hu.csv";
            var locations = Location.LoadLocationsFromCsv(fileName);

            context.Locations.AddRange(locations);
            await context.SaveChangesAsync();
        }
        
        // Seed categories
        if (!context.Categories.Any())
        {
            var fileName = "Category.csv";

            var categories = Category.LoadCategoriesFromCsv(fileName);
            context.Categories.AddRange(categories);
            await context.SaveChangesAsync();
        }
        
        // Seed roles
        var roles = new List<IdentityRole>()
        {
            new() { Name = "User" },
            new() { Name = "Admin" }
        };
        
        foreach (var identityRole in roles)
        {
            await roleManager.CreateAsync(identityRole);
        }

        // Seed users
        var oldUsers = await userManager.Users.ToListAsync();
        
        foreach (var oldUser in oldUsers)
        {
            await userManager.DeleteAsync(oldUser);
        }
        
        var newUsers = new List<User>()
        {
            new() { UserName = "Loci", Email = "loci@gmail.com" },
            new() { UserName = "Zsofi", Email = "zsofi@gmail.com" },
            new() { UserName = "Bianka", Email = "bianka@gmail.com" }
        };
        
        foreach (var newUser in newUsers)
        {
            await userManager.CreateAsync(newUser, "Abcd@1234");
            await userManager.AddToRolesAsync(newUser, new string[]{ "User","Admin" });
        }

    }
}
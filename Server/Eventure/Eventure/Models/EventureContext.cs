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
    
    public EventureContext(DbContextOptions<EventureContext> options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Event>()
            .HasOne(e => e.Creator) // An Event has one Creator (User)
            .WithMany(u => u.CreatedEvents) // A User can have many CreatedEvents
            .HasForeignKey(e => e.CreatorId) // The foreign key for the relationship
            .OnDelete(DeleteBehavior.Restrict); // You can use Restrict, SetNull, or Cascade depending on your requirements

        modelBuilder.Entity<Event>()
            .HasMany(e => e.Participants) // An Event can have many Participants (Users)
            .WithMany(u => u.AppliedEvents) // A User can have many AppliedEvents
            .UsingEntity<Dictionary<string, object>>(
                "EventParticipants", // This is the name of the join table
                j => j
                    .HasOne<User>()
                    .WithMany()
                    .HasForeignKey("UserId")
                    .HasConstraintName("FK_EventParticipants_Users_UserId")
                    .OnDelete(DeleteBehavior.Cascade), // Use Cascade if you want to delete Participant records when a User is deleted
                j => j
                    .HasOne<Event>()
                    .WithMany()
                    .HasForeignKey("EventId")
                    .HasConstraintName("FK_EventParticipants_Events_EventId")
                    .OnDelete(DeleteBehavior.Cascade) // Use Cascade if you want to delete Participant records when an Event is deleted
            );
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
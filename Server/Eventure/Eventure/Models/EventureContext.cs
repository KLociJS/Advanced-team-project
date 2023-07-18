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
    }
}
using EventManagement.Domain.Entity;
using EventManagement.Infrastuture.DataBase.SeedData;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EventManagement.Infrastuture.DataBase;

public class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext<User>(options)
{
    
    public DbSet<Event> Events { get; set; }
    public DbSet<Booking> Bookings { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new RoleSeedData());
        modelBuilder.ApplyConfiguration(new UserSeedData());
        modelBuilder.ApplyConfiguration(new IdentityRoleDataSeedConfig());
        //modelBuilder.ApplyConfiguration(new BookingSeedData());
        modelBuilder.Entity<Booking>()
       .HasOne(b => b.User)
       .WithMany(u => u.Bookings)
       .HasForeignKey(b => b.UserId)
       .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Booking>()
            .HasOne(b => b.Event)
            .WithMany(e => e.Bookings)
            .HasForeignKey(b => b.EventId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
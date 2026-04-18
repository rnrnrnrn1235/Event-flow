using Eventflow.Models;
using Microsoft.EntityFrameworkCore;
namespace Eventflow.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Events> Events { get; set; }
    public DbSet<Ticket> Tickets { get; set; }
     public DbSet<Review>Reviews       { get; set; }
    public DbSet<Notification> Notifications { get; set; }
    public DbSet<watchlist> Watchlist { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email).IsUnique();
            
            // Configure Role enum to be stored as string
            modelBuilder.Entity<User>()
                .Property(u => u.Role)
                .HasConversion<string>();
            
            //one watchlist per user for each event
            modelBuilder.Entity<watchlist>()
                .HasIndex(w => new { w.UserId, w.EventId }).IsUnique();
            //one review per user / event
            modelBuilder.Entity<Review>()
                .HasIndex(r => new { r.UserId, r.EventId }).IsUnique();

                
            modelBuilder.Entity<Ticket>()
            //store this as a decimal with 2 decimal places and max 10 digits
            .Property(t => t.pricePaid).HasPrecision(10, 2);
            
            modelBuilder.Entity<Events>()
            .Property(e => e.ticketPrice).HasPrecision(10, 2);
            
             }
        }
    }
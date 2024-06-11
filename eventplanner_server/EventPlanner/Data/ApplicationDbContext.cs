using EventPlanner.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Event> Events { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<Poll> Polls { get; set; }
    public DbSet<Notification> Notifications { get; set; }
    public DbSet<MediaFile> MediaFiles { get; set; }
    public DbSet<Subscription> Subscriptions { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<User>(entity => { entity.ToTable(name: "Users"); });
        builder.Entity<IdentityRole<Guid>>(entity => { entity.ToTable(name: "Roles"); });
        builder.Entity<IdentityUserRole<Guid>>(entity => { entity.ToTable("UserRoles"); });
        builder.Entity<IdentityUserClaim<Guid>>(entity => { entity.ToTable("UserClaims"); });
        builder.Entity<IdentityUserLogin<Guid>>(entity => { entity.ToTable("UserLogins"); });
        builder.Entity<IdentityRoleClaim<Guid>>(entity => { entity.ToTable("RoleClaims"); });
        builder.Entity<IdentityUserToken<Guid>>(entity => { entity.ToTable("UserTokens"); });

        // Configuration for Event
        builder.Entity<Event>(entity =>
        {
            entity.ToTable("Events");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Title).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Description).HasMaxLength(1000);
            entity.Property(e => e.StartDate).IsRequired();
            entity.Property(e => e.EndDate).IsRequired();
            entity.Property(e => e.Location).HasMaxLength(200);
        });

        // Configuration for Group
        builder.Entity<Group>(entity =>
        {
            entity.ToTable("Groups");
            entity.HasKey(g => g.Id);
            entity.Property(g => g.Name).IsRequired().HasMaxLength(100);
        });

        // Configuration for Poll
        builder.Entity<Poll>(entity =>
        {
            entity.ToTable("Polls");
            entity.HasKey(p => p.Id);
            entity.Property(p => p.Question).IsRequired().HasMaxLength(500);
        });

        // Configuration for Notification
        builder.Entity<Notification>(entity =>
        {
            entity.ToTable("Notifications");
            entity.HasKey(n => n.Id);
            entity.Property(n => n.Message).IsRequired().HasMaxLength(500);
            entity.Property(n => n.Date).IsRequired();
        });

        // Configuration for File
        builder.Entity<MediaFile>(entity =>
        {
            entity.ToTable("MediaFiles");
            entity.HasKey(f => f.Id);
            entity.Property(f => f.FileName).IsRequired().HasMaxLength(255);
            entity.Property(f => f.FilePath).IsRequired().HasMaxLength(500);
            entity.Property(f => f.UploadDate).IsRequired();
        });

        // Configuration for Subscription
        builder.Entity<Subscription>(entity =>
        {
            entity.ToTable("Subscriptions");
            entity.HasKey(s => s.Id);
            entity.Property(s => s.UserId).IsRequired();
            entity.Property(s => s.Plan).IsRequired().HasMaxLength(100);
            entity.Property(s => s.Price).IsRequired().HasColumnType("decimal(18,2)");
            entity.Property(s => s.StartDate).IsRequired();
            entity.Property(s => s.EndDate).IsRequired();
        });
    }
}

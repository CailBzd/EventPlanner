using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using EventPlanner.Models;

public static class SeedData
{
    public static async Task Initialize(IServiceProvider aServiceProvider, UserManager<User> aUserManager, RoleManager<IdentityRole<Guid>> aRoleManager)
    {
        using (var vScope = aServiceProvider.CreateScope())
        {
            var vServices = vScope.ServiceProvider;
            var vContext = vServices.GetRequiredService<ApplicationDbContext>();

            vContext.Database.Migrate();

            // Seed Roles
            if (!await vContext.Roles.AnyAsync())
            {
                await aRoleManager.CreateAsync(new IdentityRole<Guid>("Admin"));
                await aRoleManager.CreateAsync(new IdentityRole<Guid>("User"));
            }

            // Seed Users
            if (!await vContext.Users.AnyAsync())
            {
                var vUser = new User
                {
                    UserName = "testuser",
                    Email = "testuser@example.com"
                };
                await aUserManager.CreateAsync(vUser, "Password123!");
                await aUserManager.AddToRoleAsync(vUser, "User");
            }

            // Seed Groups
            if (!await vContext.Groups.AnyAsync())
            {
                var vGroup = new Group
                {
                    Id = Guid.NewGuid(),
                    Name = "Test Group"
                };
                await vContext.Groups.AddAsync(vGroup);
                await vContext.SaveChangesAsync();
            }

            // Seed Polls
            if (!await vContext.Polls.AnyAsync())
            {
                var vPoll = new Poll
                {
                    Id = Guid.NewGuid(),
                    Question = "Test Poll Question"
                };
                await vContext.Polls.AddAsync(vPoll);
                await vContext.SaveChangesAsync();
            }

            // Seed Files
            if (!await vContext.MediaFiles.AnyAsync())
            {
                var vFile = new MediaFile
                {
                    Id = Guid.NewGuid(),
                    FileName = "testfile.txt",
                    FilePath = "uploads/testfile.txt",
                    UploadDate = DateTime.UtcNow
                };
                await vContext.MediaFiles.AddAsync(vFile);
                await vContext.SaveChangesAsync();
            }

            // Seed Notifications
            if (!await vContext.Notifications.AnyAsync())
            {
                var vNotification = new Notification
                {
                    Id = Guid.NewGuid(),
                    Message = "Test Notification",
                    Date = DateTime.UtcNow
                };
                await vContext.Notifications.AddAsync(vNotification);
                await vContext.SaveChangesAsync();
            }

            // Seed Subscriptions
            if (!await vContext.Subscriptions.AnyAsync())
            {
                var vUser = await vContext.Users.FirstAsync();
                var vSubscription = new Subscription
                {
                    Id = Guid.NewGuid(),
                    UserId = vUser.Id,
                    Plan = "Test Plan",
                    StartDate = DateTime.UtcNow,
                    EndDate = DateTime.UtcNow.AddYears(1)
                };
                await vContext.Subscriptions.AddAsync(vSubscription);
                await vContext.SaveChangesAsync();
            }
        }
    }
}

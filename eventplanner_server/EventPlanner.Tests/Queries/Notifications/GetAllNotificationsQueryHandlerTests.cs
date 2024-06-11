using Xunit;
using EventPlanner.Models;
using Microsoft.EntityFrameworkCore;
using EventPlanner.Features;

namespace EventPlanner.Tests;

public class GetAllNotificationsQueryHandlerTests
{
    private readonly ApplicationDbContext _context;
    private readonly GetAllNotificationsQueryHandler _handler;

    public GetAllNotificationsQueryHandlerTests()
    {
        var vOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        _context = new ApplicationDbContext(vOptions);
        _handler = new GetAllNotificationsQueryHandler(_context);
    }

    private async Task SeedDatabase()
    {
        var vNotifications = new List<Notification>
        {
            new Notification { Id = Guid.NewGuid(), Message = "Notification 1", Date = DateTime.UtcNow },
            new Notification { Id = Guid.NewGuid(), Message = "Notification 2", Date = DateTime.UtcNow }
        };

        await _context.Notifications.AddRangeAsync(vNotifications);
        await _context.SaveChangesAsync();
    }
    [Fact]
    public async Task Handle_ShouldReturnListOfNotificationDetails()
    {
        // Arrange
        await SeedDatabase();


        // Act
        var vResult = await _handler.Handle(new GetAllNotificationsQuery(), CancellationToken.None);

        // Assert
        Assert.NotNull(vResult);
        Assert.Equal(2, vResult.Count);
        Assert.Contains(vResult, n => n.Message == "Notification 1");
        Assert.Contains(vResult, n => n.Message == "Notification 2");
    }
}

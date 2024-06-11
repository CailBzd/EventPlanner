using Xunit;
using Microsoft.EntityFrameworkCore;
using EventPlanner.Features;

namespace EventPlanner.Tests;

public class CreateNotificationCommandHandlerTests
{
    private readonly ApplicationDbContext _context;
    private readonly CreateNotificationCommandHandler _handler;

    public CreateNotificationCommandHandlerTests()
    {
        var vOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;
        _context = new ApplicationDbContext(vOptions);
        _handler = new CreateNotificationCommandHandler(_context);
    }

    [Fact]
    public async Task Handle_GivenValidRequest_ShouldCreateNotification()
    {
        // Arrange
        var aCommand = new CreateNotificationCommand
        {
            Message = "Test Notification",
            Date = DateTime.UtcNow
        };

        // Act
        var vResult = await _handler.Handle(aCommand, CancellationToken.None);

        // Assert
        Assert.NotEqual(Guid.Empty, vResult);
        Assert.NotNull(await _context.Notifications.FindAsync(vResult));
    }
}

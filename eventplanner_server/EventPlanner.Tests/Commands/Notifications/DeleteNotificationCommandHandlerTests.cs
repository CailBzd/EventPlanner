using Xunit;
using EventPlanner.Models;
using Microsoft.EntityFrameworkCore;
using EventPlanner.Features;

namespace EventPlanner.Tests;

public class DeleteNotificationCommandHandlerTests
{
    private readonly ApplicationDbContext _context;
    private readonly DeleteNotificationCommandHandler _handler;

    public DeleteNotificationCommandHandlerTests()
    {
        var vOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;
        _context = new ApplicationDbContext(vOptions);
        _handler = new DeleteNotificationCommandHandler(_context);
    }

    [Fact]
    public async Task Handle_GivenValidRequest_ShouldDeleteNotification()
    {
        // Arrange
        var vNotificationId = Guid.NewGuid();
        var vNotificationEntity = new Notification
        {
            Id = vNotificationId,
            Message = "Test Notification",
            Date = DateTime.UtcNow
        };

        await _context.Notifications.AddAsync(vNotificationEntity);
        await _context.SaveChangesAsync();

        var aCommand = new DeleteNotificationCommand { Id = vNotificationId };

        // Act
        var vResult = await _handler.Handle(aCommand, CancellationToken.None);

        // Assert
        Assert.True(vResult);
        var vDeletedNotification = await _context.Notifications.FindAsync(vNotificationId);
        Assert.Null(vDeletedNotification);
    }

    [Fact]
    public async Task Handle_GivenInvalidRequest_ShouldReturnFalse()
    {
        // Arrange
        var aCommand = new DeleteNotificationCommand { Id = Guid.NewGuid() };

        // Act
        var vResult = await _handler.Handle(aCommand, CancellationToken.None);

        // Assert
        Assert.False(vResult);
    }
}

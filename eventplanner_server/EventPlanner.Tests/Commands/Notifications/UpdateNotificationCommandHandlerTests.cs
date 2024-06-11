using Xunit;
using EventPlanner.Models;
using Microsoft.EntityFrameworkCore;
using EventPlanner.Features;

namespace EventPlanner.Tests;

public class UpdateNotificationCommandHandlerTests
{
    private readonly ApplicationDbContext _context;
    private readonly UpdateNotificationCommandHandler _handler;

    public UpdateNotificationCommandHandlerTests()
    {
        var vOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;
        _context = new ApplicationDbContext(vOptions);
        _handler = new UpdateNotificationCommandHandler(_context);
    }

    [Fact]
    public async Task Handle_GivenValidRequest_ShouldUpdateNotification()
    {
        // Arrange
        var vNotificationId = Guid.NewGuid();
        var vNotificationEntity = new Notification
        {
            Id = vNotificationId,
            Message = "Original Message",
            Date = DateTime.UtcNow
        };

        await _context.Notifications.AddAsync(vNotificationEntity);
        await _context.SaveChangesAsync();

        var aCommand = new UpdateNotificationCommand
        {
            Id = vNotificationId,
            Message = "Updated Message",
            Date = DateTime.UtcNow.AddDays(1)
        };

        // Act
        var vResult = await _handler.Handle(aCommand, CancellationToken.None);

        // Assert
        Assert.True(vResult);
        var vUpdatedNotification = await _context.Notifications.FindAsync(vNotificationId);
        Assert.Equal("Updated Message", vUpdatedNotification.Message);
    }

    [Fact]
    public async Task Handle_GivenInvalidRequest_ShouldReturnFalse()
    {
        // Arrange
        var aCommand = new UpdateNotificationCommand
        {
            Id = Guid.NewGuid(),
            Message = "Updated Message",
            Date = DateTime.UtcNow.AddDays(1)
        };

        // Act
        var vResult = await _handler.Handle(aCommand, CancellationToken.None);

        // Assert
        Assert.False(vResult);
    }
}

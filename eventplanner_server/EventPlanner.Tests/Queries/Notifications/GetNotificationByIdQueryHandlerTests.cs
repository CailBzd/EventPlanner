using Xunit;
using EventPlanner.Models;
using Microsoft.EntityFrameworkCore;
using EventPlanner.Features;

namespace EventPlanner.Tests;

public class GetNotificationByIdQueryHandlerTests
{
    private readonly ApplicationDbContext _context;
    private readonly GetNotificationByIdQueryHandler _handler;

    public GetNotificationByIdQueryHandlerTests()
    {
        var vOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;
        _context = new ApplicationDbContext(vOptions);
        _handler = new GetNotificationByIdQueryHandler(_context);
    }

    [Fact]
    public async Task Handle_GivenValidId_ShouldReturnNotificationDetail()
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

        var aQuery = new GetNotificationByIdQuery { Id = vNotificationId };

        // Act
        var vResult = await _handler.Handle(aQuery, CancellationToken.None);

        // Assert
        Assert.NotNull(vResult);
        Assert.Equal(vNotificationId, vResult.Id);
        Assert.Equal("Test Notification", vResult.Message);
    }

    [Fact]
    public async Task Handle_GivenInvalidId_ShouldReturnNull()
    {
        // Arrange
        var aQuery = new GetNotificationByIdQuery { Id = Guid.NewGuid() };

        // Act
        var vResult = await _handler.Handle(aQuery, CancellationToken.None);

        // Assert
        Assert.Null(vResult);
    }
}

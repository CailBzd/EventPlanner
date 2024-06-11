using Xunit;
using EventPlanner.Models;
using Microsoft.EntityFrameworkCore;
using EventPlanner.Features;

namespace EventPlanner.Tests;

public class UpdateSubscriptionCommandHandlerTests
{
    private readonly ApplicationDbContext _context;
    private readonly UpdateSubscriptionCommandHandler _handler;

    public UpdateSubscriptionCommandHandlerTests()
    {
        var vOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;
        _context = new ApplicationDbContext(vOptions);
        _handler = new UpdateSubscriptionCommandHandler(_context);
    }

    [Fact]
    public async Task Handle_GivenValidRequest_ShouldUpdateSubscription()
    {
        // Arrange
        var vSubscriptionId = Guid.NewGuid();
        var vSubscriptionEntity = new Subscription
        {
            Id = vSubscriptionId,
            UserId = Guid.NewGuid(),
            Plan = "Original Plan",
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddYears(1)
        };

        await _context.Subscriptions.AddAsync(vSubscriptionEntity);
        await _context.SaveChangesAsync();

        var aCommand = new UpdateSubscriptionCommand
        {
            Id = vSubscriptionId,
            Plan = "Updated Plan",
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddYears(2)
        };

        // Act
        var vResult = await _handler.Handle(aCommand, CancellationToken.None);

        // Assert
        Assert.True(vResult);
        var vUpdatedSubscription = await _context.Subscriptions.FindAsync(vSubscriptionId);
        Assert.Equal("Updated Plan", vUpdatedSubscription.Plan);
    }

    [Fact]
    public async Task Handle_GivenInvalidRequest_ShouldReturnFalse()
    {
        // Arrange
        var aCommand = new UpdateSubscriptionCommand
        {
            Id = Guid.NewGuid(),
            Plan = "Updated Plan",
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddYears(2)
        };

        // Act
        var vResult = await _handler.Handle(aCommand, CancellationToken.None);

        // Assert
        Assert.False(vResult);
    }
}

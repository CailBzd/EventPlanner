using Xunit;
using EventPlanner.Models;
using Microsoft.EntityFrameworkCore;
using EventPlanner.Features;

namespace EventPlanner.Tests;

public class DeleteSubscriptionCommandHandlerTests
{
    private readonly ApplicationDbContext _context;
    private readonly DeleteSubscriptionCommandHandler _handler;

    public DeleteSubscriptionCommandHandlerTests()
    {
        var vOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;
        _context = new ApplicationDbContext(vOptions);
        _handler = new DeleteSubscriptionCommandHandler(_context);
    }

    [Fact]
    public async Task Handle_GivenValidRequest_ShouldDeleteSubscription()
    {
        // Arrange
        var vSubscriptionId = Guid.NewGuid();
        var vSubscriptionEntity = new Subscription
        {
            Id = vSubscriptionId,
            UserId = Guid.NewGuid(),
            Plan = "Test Plan",
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddYears(1)
        };

        await _context.Subscriptions.AddAsync(vSubscriptionEntity);
        await _context.SaveChangesAsync();

        var aCommand = new DeleteSubscriptionCommand { Id = vSubscriptionId };

        // Act
        var vResult = await _handler.Handle(aCommand, CancellationToken.None);

        // Assert
        Assert.True(vResult);
        var vDeletedSubscription = await _context.Subscriptions.FindAsync(vSubscriptionId);
        Assert.Null(vDeletedSubscription);
    }

    [Fact]
    public async Task Handle_GivenInvalidRequest_ShouldReturnFalse()
    {
        // Arrange
        var aCommand = new DeleteSubscriptionCommand { Id = Guid.NewGuid() };

        // Act
        var vResult = await _handler.Handle(aCommand, CancellationToken.None);

        // Assert
        Assert.False(vResult);
    }
}

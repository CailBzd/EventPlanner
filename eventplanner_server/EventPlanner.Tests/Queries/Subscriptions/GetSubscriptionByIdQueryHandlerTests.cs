using Xunit;
using EventPlanner.Models;
using Microsoft.EntityFrameworkCore;
using EventPlanner.Features;

namespace EventPlanner.Tests;

public class GetSubscriptionByIdQueryHandlerTests
{
    private readonly ApplicationDbContext _context;
    private readonly GetSubscriptionByIdQueryHandler _handler;

    public GetSubscriptionByIdQueryHandlerTests()
    {
        var vOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;
        _context = new ApplicationDbContext(vOptions);
        _handler = new GetSubscriptionByIdQueryHandler(_context);
    }

    [Fact]
    public async Task Handle_GivenValidId_ShouldReturnSubscriptionDetail()
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

        var aQuery = new GetSubscriptionByIdQuery { Id = vSubscriptionId };

        // Act
        var vResult = await _handler.Handle(aQuery, CancellationToken.None);

        // Assert
        Assert.NotNull(vResult);
        Assert.Equal(vSubscriptionId, vResult.Id);
        Assert.Equal("Test Plan", vResult.Plan);
    }

    [Fact]
    public async Task Handle_GivenInvalidId_ShouldReturnNull()
    {
        // Arrange
        var aQuery = new GetSubscriptionByIdQuery { Id = Guid.NewGuid() };

        // Act
        var vResult = await _handler.Handle(aQuery, CancellationToken.None);

        // Assert
        Assert.Null(vResult);
    }
}

using Xunit;
using EventPlanner.Models;
using Microsoft.EntityFrameworkCore;
using EventPlanner.Features;

namespace EventPlanner.Tests;

public class GetAllSubscriptionsQueryHandlerTests
{
    private readonly ApplicationDbContext _context;
    private readonly GetAllSubscriptionsQueryHandler _handler;

    public GetAllSubscriptionsQueryHandlerTests()
    {
        var vOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;
        _context = new ApplicationDbContext(vOptions);
        _handler = new GetAllSubscriptionsQueryHandler(_context);
    }

    private async Task SeedDatabase()
    {
        var vSubscriptions = new List<Subscription>
        {
            new Subscription { Id = Guid.NewGuid(), UserId = Guid.NewGuid(), Plan = "Plan 1", Price =5, StartDate = DateTime.UtcNow, EndDate = DateTime.UtcNow.AddYears(1) },
            new Subscription { Id = Guid.NewGuid(), UserId = Guid.NewGuid(), Plan = "Plan 2", Price =10, StartDate = DateTime.UtcNow, EndDate = DateTime.UtcNow.AddYears(1) }
        };

        await _context.Subscriptions.AddRangeAsync(vSubscriptions);
        await _context.SaveChangesAsync();
    }
    [Fact]
    public async Task Handle_ShouldReturnListOfSubscriptionDetails()
    {
        // Arrange
        await SeedDatabase();

        // Act
        var vResult = await _handler.Handle(new GetAllSubscriptionsQuery(), CancellationToken.None);

        // Assert
        Assert.NotNull(vResult);
        Assert.Equal(2, vResult.Count);
        Assert.Contains(vResult, e => e.Plan == "Plan 1");
        Assert.Contains(vResult, e => e.Plan == "Plan 2");
        Assert.Contains(vResult, e => e.Price == 5);
        Assert.Contains(vResult, e => e.Price == 10);
    }
}

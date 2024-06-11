using Xunit;
using Microsoft.EntityFrameworkCore;
using EventPlanner.Features;

namespace EventPlanner.Tests;

public class CreateSubscriptionCommandHandlerTests
{
    private readonly ApplicationDbContext _context;
    private readonly CreateSubscriptionCommandHandler _handler;

    public CreateSubscriptionCommandHandlerTests()
    {
        var vOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;
        _context = new ApplicationDbContext(vOptions);
        _handler = new CreateSubscriptionCommandHandler(_context);
    }

    [Fact]
    public async Task Handle_GivenValidRequest_ShouldCreateSubscription()
    {
        // Arrange
        var aCommand = new CreateSubscriptionCommand
        {
            UserId = Guid.NewGuid(),
            Plan = "Test Plan",
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddYears(1)
        };

        // Act
        var vResult = await _handler.Handle(aCommand, CancellationToken.None);

        // Assert
        Assert.NotEqual(Guid.Empty, vResult);
        Assert.NotNull(await _context.Subscriptions.FindAsync(vResult));
    }
}

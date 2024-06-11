using Xunit;
using Moq;
using Microsoft.EntityFrameworkCore;
using EventPlanner.Features;

namespace EventPlanner.Tests;

public class CreateEventCommandHandlerTests
{
    private readonly ApplicationDbContext _context;
    private readonly CreateEventCommandHandler _handler;

    public CreateEventCommandHandlerTests()
    {
        var vOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        _context = new ApplicationDbContext(vOptions);
        _handler = new CreateEventCommandHandler(_context);
    }


    [Fact]
    public async Task Handle_GivenValidRequest_ShouldCreateEvent()
    {
        // Arrange
        var vEvent = new CreateEventCommand
        {
            Title = "Test Event",
            Description = "Test Description",
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddDays(1),
            Location = "Test Location"
        };

        // Act
        var vResult = await _handler.Handle(vEvent, CancellationToken.None);

        // Assert
        Assert.NotEqual(Guid.Empty, vResult);
        var vCreatedEvent = await _context.Events.FindAsync(vResult);
        Assert.NotNull(vCreatedEvent);
        Assert.Equal(vEvent.Title, vCreatedEvent.Title);
        Assert.Equal(vEvent.Description, vCreatedEvent.Description);
        Assert.Equal(vEvent.StartDate, vCreatedEvent.StartDate);
        Assert.Equal(vEvent.EndDate, vCreatedEvent.EndDate);
        Assert.Equal(vEvent.Location, vCreatedEvent.Location);
    }
}

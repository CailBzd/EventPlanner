using Xunit;
using Moq;
using EventPlanner.Models;
using Microsoft.EntityFrameworkCore;
using EventPlanner.Features;

namespace EventPlanner.Tests;

public class UpdateEventCommandHandlerTests
{
    private readonly ApplicationDbContext _context;
    private readonly UpdateEventCommandHandler _handler;

    public UpdateEventCommandHandlerTests()
    {
        var vOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;
        var vContext = new ApplicationDbContext(vOptions);

        _context = new ApplicationDbContext(vOptions);
        _handler = new UpdateEventCommandHandler(_context);
    }

    [Fact]
    public async Task Handle_GivenValidRequest_ShouldUpdateEvent()
    {
        // Arrange
        var vEventId = Guid.NewGuid();
        var vEventEntity = new Event
        {
            Id = vEventId,
            Title = "Original Title",
            Description = "Original Description",
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddDays(1),
            Location = "Original Location"
        };

        await _context.Events.AddAsync(vEventEntity);
        await _context.SaveChangesAsync();

        var aCommand = new UpdateEventCommand
        {
            Id = vEventId,
            Title = "Updated Title",
            Description = "Updated Description",
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddDays(2),
            Location = "Updated Location"
        };

        // Act
        var vResult = await _handler.Handle(aCommand, CancellationToken.None);

        // Assert
        Assert.True(vResult);
        var vUpdatedEvent = await _context.Events.FindAsync(vEventId);
        Assert.NotNull(vUpdatedEvent);
        Assert.Equal(aCommand.Title, vUpdatedEvent.Title);
        Assert.Equal(aCommand.Description, vUpdatedEvent.Description);
        Assert.Equal(aCommand.StartDate, vUpdatedEvent.StartDate);
        Assert.Equal(aCommand.Location, vUpdatedEvent.Location);
    }

    [Fact]
    public async Task Handle_GivenInvalidRequest_ShouldReturnFalse()
    {
        // Arrange
        var aCommand = new UpdateEventCommand
        {
            Id = Guid.NewGuid(),
            Title = "Updated Title",
            Description = "Updated Description",
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddDays(2),
            Location = "Updated Location"
        };

        // Act
        var vResult = await _handler.Handle(aCommand, CancellationToken.None);

        // Assert
        Assert.False(vResult);
    }
}

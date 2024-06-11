using Xunit;
using Moq;
using EventPlanner.Models;
using Microsoft.EntityFrameworkCore;
using EventPlanner.Features;

namespace EventPlanner.Tests;

public class DeleteEventCommandHandlerTests
{
    private readonly ApplicationDbContext _context;
    private readonly DeleteEventCommandHandler _handler;

    public DeleteEventCommandHandlerTests()
    {
        var vOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        _context = new ApplicationDbContext(vOptions);
        _handler = new DeleteEventCommandHandler(_context);
    }

    private async Task SeedDatabase()
    {
        var events = new List<Event>
            {
                new Event { Id = Guid.NewGuid(), Title = "Event1", Description = "Description1", StartDate = DateTime.UtcNow, EndDate = DateTime.UtcNow.AddDays(7), Location = "TestLocation" },
                new Event { Id = Guid.NewGuid(), Title = "Event2", Description = "Description2", StartDate = DateTime.UtcNow, EndDate = DateTime.UtcNow.AddDays(7), Location = "TestLocation"}
            };

        await _context.Events.AddRangeAsync(events);
        await _context.SaveChangesAsync();
    }

    [Fact]
    public async Task Handle_GivenValidRequest_ShouldDeleteEvent()
    {
        // Arrange
        await SeedDatabase();
        var vEventId = _context.Events.First().Id;

        var aCommand = new DeleteEventCommand { Id = vEventId };

        // Act
        var vResult = await _handler.Handle(aCommand, CancellationToken.None);

        // Assert
        Assert.True(vResult);
        var vDeletedEvent = await _context.Events.FindAsync(vEventId);
        Assert.Null(vDeletedEvent);
    }

    [Fact]
    public async Task Handle_GivenInvalidRequest_ShouldReturnFalse()
    {
        // Arrange
        await SeedDatabase();
        var aCommand = new DeleteEventCommand { Id = Guid.NewGuid() };

        // Act
        var vResult = await _handler.Handle(aCommand, CancellationToken.None);

        // Assert
        Assert.False(vResult);
    }
}

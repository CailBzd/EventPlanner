using Xunit;
using EventPlanner.Models;
using Microsoft.EntityFrameworkCore;
using EventPlanner.Features;

namespace EventPlanner.Tests;

public class UpdatePollCommandHandlerTests
{
    private readonly ApplicationDbContext _context;
    private readonly UpdatePollCommandHandler _handler;

    public UpdatePollCommandHandlerTests()
    {
        var vOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;
        _context = new ApplicationDbContext(vOptions);
        _handler = new UpdatePollCommandHandler(_context);
    }

    [Fact]
    public async Task Handle_GivenValidRequest_ShouldUpdatePoll()
    {
        // Arrange
        var vPollId = Guid.NewGuid();
        var vPollEntity = new Poll
        {
            Id = vPollId,
            Question = "Original Question"
        };

        await _context.Polls.AddAsync(vPollEntity);
        await _context.SaveChangesAsync();

        var aCommand = new UpdatePollCommand
        {
            Id = vPollId,
            Question = "Updated Question"
        };

        // Act
        var vResult = await _handler.Handle(aCommand, CancellationToken.None);

        // Assert
        Assert.True(vResult);
        var vUpdatedPoll = await _context.Polls.FindAsync(vPollId);
        Assert.Equal("Updated Question", vUpdatedPoll.Question);
    }

    [Fact]
    public async Task Handle_GivenInvalidRequest_ShouldReturnFalse()
    {
        // Arrange
        var aCommand = new UpdatePollCommand
        {
            Id = Guid.NewGuid(),
            Question = "Updated Question"
        };

        // Act
        var vResult = await _handler.Handle(aCommand, CancellationToken.None);

        // Assert
        Assert.False(vResult);
    }
}

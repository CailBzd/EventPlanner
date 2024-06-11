using Xunit;
using EventPlanner.Models;
using Microsoft.EntityFrameworkCore;
using EventPlanner.Features;

namespace EventPlanner.Tests;

public class DeletePollCommandHandlerTests
{
    private readonly ApplicationDbContext _context;
    private readonly DeletePollCommandHandler _handler;

    public DeletePollCommandHandlerTests()
    {
        var vOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;
        _context = new ApplicationDbContext(vOptions);
        _handler = new DeletePollCommandHandler(_context);
    }

    [Fact]
    public async Task Handle_GivenValidRequest_ShouldDeletePoll()
    {
        // Arrange
        var vPollId = Guid.NewGuid();
        var vPollEntity = new Poll
        {
            Id = vPollId,
            Question = "Test Poll"
        };

        await _context.Polls.AddAsync(vPollEntity);
        await _context.SaveChangesAsync();

        var aCommand = new DeletePollCommand { Id = vPollId };

        // Act
        var vResult = await _handler.Handle(aCommand, CancellationToken.None);

        // Assert
        Assert.True(vResult);
        var vDeletedPoll = await _context.Polls.FindAsync(vPollId);
        Assert.Null(vDeletedPoll);
    }

    [Fact]
    public async Task Handle_GivenInvalidRequest_ShouldReturnFalse()
    {
        // Arrange
        var aCommand = new DeletePollCommand { Id = Guid.NewGuid() };

        // Act
        var vResult = await _handler.Handle(aCommand, CancellationToken.None);

        // Assert
        Assert.False(vResult);
    }
}

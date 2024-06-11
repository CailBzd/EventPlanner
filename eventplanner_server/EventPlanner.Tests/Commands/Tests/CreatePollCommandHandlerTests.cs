using Xunit;
using Microsoft.EntityFrameworkCore;
using EventPlanner.Features;

namespace EventPlanner.Tests;

public class CreatePollCommandHandlerTests
{
    private readonly ApplicationDbContext _context;
    private readonly CreatePollCommandHandler _handler;

    public CreatePollCommandHandlerTests()
    {
        var vOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;
        _context = new ApplicationDbContext(vOptions);
        _handler = new CreatePollCommandHandler(_context);
    }

    [Fact]
    public async Task Handle_GivenValidRequest_ShouldCreatePoll()
    {
        // Arrange
        var aCommand = new CreatePollCommand
        {
            Question = "Test Question"
        };

        // Act
        var vResult = await _handler.Handle(aCommand, CancellationToken.None);

        // Assert
        Assert.NotEqual(Guid.Empty, vResult);
        Assert.NotNull(await _context.Polls.FindAsync(vResult));
    }
}

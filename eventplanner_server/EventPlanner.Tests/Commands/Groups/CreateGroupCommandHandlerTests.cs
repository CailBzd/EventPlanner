using Xunit;
using Microsoft.EntityFrameworkCore;
using EventPlanner.Features;

namespace EventPlanner.Tests;

public class CreateGroupCommandHandlerTests
{
    private readonly ApplicationDbContext _context;
    private readonly CreateGroupCommandHandler _handler;

    public CreateGroupCommandHandlerTests()
    {
        var vOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;
        _context = new ApplicationDbContext(vOptions);
        _handler = new CreateGroupCommandHandler(_context);
    }

    [Fact]
    public async Task Handle_GivenValidRequest_ShouldCreateGroup()
    {
        // Arrange
        var aCommand = new CreateGroupCommand
        {
            Name = "Test Group"
        };

        // Act
        var vResult = await _handler.Handle(aCommand, CancellationToken.None);

        // Assert
        Assert.NotEqual(Guid.Empty, vResult);
        Assert.NotNull(await _context.Groups.FindAsync(vResult));
    }
}

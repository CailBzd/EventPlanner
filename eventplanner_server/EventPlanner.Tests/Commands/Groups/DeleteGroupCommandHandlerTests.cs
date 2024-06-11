using Xunit;
using EventPlanner.Models;
using Microsoft.EntityFrameworkCore;
using EventPlanner.Features;

namespace EventPlanner.Tests;

public class DeleteGroupCommandHandlerTests
{
    private readonly ApplicationDbContext _context;
    private readonly DeleteGroupCommandHandler _handler;

    public DeleteGroupCommandHandlerTests()
    {
        var vOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;
        _context = new ApplicationDbContext(vOptions);
        _handler = new DeleteGroupCommandHandler(_context);
    }

    [Fact]
    public async Task Handle_GivenValidRequest_ShouldDeleteGroup()
    {
        // Arrange
        var vGroupId = Guid.NewGuid();
        var vGroupEntity = new Group
        {
            Id = vGroupId,
            Name = "Test Group"
        };

        await _context.Groups.AddAsync(vGroupEntity);
        await _context.SaveChangesAsync();

        var aCommand = new DeleteGroupCommand
        {
            Id = vGroupId
        };

        // Act
        var vResult = await _handler.Handle(aCommand, CancellationToken.None);

        // Assert
        Assert.True(vResult);
        var vDeletedGroup = await _context.Groups.FindAsync(vGroupId);
        Assert.Null(vDeletedGroup);
    }

    [Fact]
    public async Task Handle_GivenInvalidRequest_ShouldReturnFalse()
    {
        // Arrange
        var aCommand = new DeleteGroupCommand
        {
            Id = Guid.NewGuid()
        };

        // Act
        var vResult = await _handler.Handle(aCommand, CancellationToken.None);

        // Assert
        Assert.False(vResult);
    }
}

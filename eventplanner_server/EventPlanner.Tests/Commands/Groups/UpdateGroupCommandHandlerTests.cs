using Xunit;
using EventPlanner.Models;
using Microsoft.EntityFrameworkCore;
using EventPlanner.Features;

namespace EventPlanner.Tests;

public class UpdateGroupCommandHandlerTests
{
    private readonly ApplicationDbContext _context;
    private readonly UpdateGroupCommandHandler _handler;

    public UpdateGroupCommandHandlerTests()
    {
        var vOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;
        _context = new ApplicationDbContext(vOptions);
        _handler = new UpdateGroupCommandHandler(_context);
    }

    [Fact]
    public async Task Handle_GivenValidRequest_ShouldUpdateGroup()
    {
        // Arrange
        var vGroupId = Guid.NewGuid();
        var vGroupEntity = new Group
        {
            Id = vGroupId,
            Name = "Original Name"
        };

        await _context.Groups.AddAsync(vGroupEntity);
        await _context.SaveChangesAsync();

        var aCommand = new UpdateGroupCommand
        {
            Id = vGroupId,
            Name = "Updated Name"
        };

        // Act
        var vResult = await _handler.Handle(aCommand, CancellationToken.None);

        // Assert
        Assert.True(vResult);
        var vUpdatedGroup = await _context.Groups.FindAsync(vGroupId);
        Assert.Equal("Updated Name", vUpdatedGroup.Name);
    }

    [Fact]
    public async Task Handle_GivenInvalidRequest_ShouldReturnFalse()
    {
        // Arrange
        var aCommand = new UpdateGroupCommand
        {
            Id = Guid.NewGuid(),
            Name = "Updated Name"
        };

        // Act
        var vResult = await _handler.Handle(aCommand, CancellationToken.None);

        // Assert
        Assert.False(vResult);
    }
}

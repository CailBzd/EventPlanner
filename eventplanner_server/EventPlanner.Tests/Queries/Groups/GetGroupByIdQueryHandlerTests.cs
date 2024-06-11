using Xunit;
using EventPlanner.Models;
using Microsoft.EntityFrameworkCore;
using EventPlanner.Features;

namespace EventPlanner.Tests;

public class GetGroupByIdQueryHandlerTests
{
    private readonly ApplicationDbContext _context;
    private readonly GetGroupByIdQueryHandler _handler;

    public GetGroupByIdQueryHandlerTests()
    {
        var vOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;
        _context = new ApplicationDbContext(vOptions);
        _handler = new GetGroupByIdQueryHandler(_context);
    }

    [Fact]
    public async Task Handle_GivenValidId_ShouldReturnGroupDetail()
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

        var aQuery = new GetGroupByIdQuery { Id = vGroupId };

        // Act
        var vResult = await _handler.Handle(aQuery, CancellationToken.None);

        // Assert
        Assert.NotNull(vResult);
        Assert.Equal(vGroupId, vResult.Id);
        Assert.Equal("Test Group", vResult.Name);
    }

    [Fact]
    public async Task Handle_GivenInvalidId_ShouldReturnNull()
    {
        // Arrange
        var aQuery = new GetGroupByIdQuery { Id = Guid.NewGuid() };

        // Act
        var vResult = await _handler.Handle(aQuery, CancellationToken.None);

        // Assert
        Assert.Null(vResult);
    }
}

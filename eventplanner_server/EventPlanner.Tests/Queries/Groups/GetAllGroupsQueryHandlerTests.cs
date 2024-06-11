using Xunit;
using EventPlanner.Models;
using Microsoft.EntityFrameworkCore;
using EventPlanner.Features;

namespace EventPlanner.Tests;

public class GetAllGroupsQueryHandlerTests
{
    private readonly ApplicationDbContext _context;
    private readonly GetAllGroupsQueryHandler _handler;

    public GetAllGroupsQueryHandlerTests()
    {
        var vOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;
        _context = new ApplicationDbContext(vOptions);
        _handler = new GetAllGroupsQueryHandler(_context);

    }

    private async Task SeedDatabase()
    {
        var groups = new List<Group>
            {
                new Group { Id = Guid.NewGuid(), Name = "Group1"},
                new Group { Id = Guid.NewGuid(), Name = "Group2" }
            };

        await _context.Groups.AddRangeAsync(groups);
        await _context.SaveChangesAsync();
    }
    [Fact]
    public async Task Handle_ShouldReturnListOfGroupDetails()
    {
        // Arrange
        await SeedDatabase();
        var vQuery = new GetAllGroupsQuery();

        // Act
        var vResult = await _handler.Handle(vQuery, CancellationToken.None);

        // Assert
        Assert.NotNull(vResult);
        Assert.Equal(2, vResult.Count);
        Assert.Contains(vResult, g => g.Name == "Group1");
        Assert.Contains(vResult, g => g.Name == "Group2");
    }
}

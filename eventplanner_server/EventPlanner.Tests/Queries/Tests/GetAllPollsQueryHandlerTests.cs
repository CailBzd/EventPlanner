using Xunit;
using EventPlanner.Models;
using Microsoft.EntityFrameworkCore;
using EventPlanner.Features;

namespace EventPlanner.Tests;

public class GetAllPollsQueryHandlerTests
{
    private readonly ApplicationDbContext _context;
    private readonly GetAllPollsQueryHandler _handler;

    public GetAllPollsQueryHandlerTests()
    {
        var vOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;
        _context = new ApplicationDbContext(vOptions);
        _handler = new GetAllPollsQueryHandler(_context);
    }

    private async Task SeedDatabase()
    {
        var vPolls = new List<Poll>
        {
            new Poll { Id = Guid.NewGuid(), Question = "Poll 1" },
            new Poll { Id = Guid.NewGuid(), Question = "Poll 2" }
        };

        await _context.Polls.AddRangeAsync(vPolls);
        await _context.SaveChangesAsync();
    }
    [Fact]
    public async Task Handle_ShouldReturnListOfPollDetails()
    {
        // Arrange
        await SeedDatabase();

        // Act
        var vResult = await _handler.Handle(new GetAllPollsQuery(), CancellationToken.None);

        // Assert
        Assert.NotNull(vResult);
        Assert.Equal(2, vResult.Count);
        Assert.Contains(vResult, e => e.Question == "Poll 1");
        Assert.Contains(vResult, e => e.Question == "Poll 2");
    }
}

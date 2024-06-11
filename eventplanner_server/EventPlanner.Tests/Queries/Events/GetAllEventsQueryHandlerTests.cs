using Moq;
using Xunit;
using Microsoft.EntityFrameworkCore;
using EventPlanner.Features;
using EventPlanner.Models;

public class GetAllEventsQueryHandlerTests
{
    private readonly ApplicationDbContext _context;
    private readonly GetAllEventsQueryHandler _handler;

    public GetAllEventsQueryHandlerTests()
    {
        var vOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        _context = new ApplicationDbContext(vOptions);

        _handler = new GetAllEventsQueryHandler(_context);
    }

    private async Task SeedDatabase()
    {
        var events = new List<Event>
            {
                new Event { Id = Guid.NewGuid(), Title = "Initial Event 1", Description = "Description 1", StartDate = DateTime.UtcNow.AddDays(1), EndDate = DateTime.UtcNow.AddDays(7), Location = "Test Location 1" },
                new Event { Id = Guid.NewGuid(), Title = "Initial Event 2", Description = "Description 2", StartDate = DateTime.UtcNow.AddDays(2), EndDate = DateTime.UtcNow.AddDays(15), Location = "Test Location 2" }
            };

        await _context.Events.AddRangeAsync(events);
        await _context.SaveChangesAsync();
    }

    [Fact]
    public async Task Handle_ShouldReturnListOfEventDetails()
    {
        // Arrange
        await SeedDatabase();

        // Act
        var vResult = await _handler.Handle(new GetAllEventsQuery(), CancellationToken.None);

        // Assert
        Assert.NotNull(vResult);
        Assert.Equal(2, vResult.Count);
        Assert.Contains(vResult, e => e.Title == "Initial Event 1");
        Assert.Contains(vResult, e => e.Title == "Initial Event 2");
        Assert.Contains(vResult, e => e.Description == "Description 1");
        Assert.Contains(vResult, e => e.Description == "Description 2");
    }
}

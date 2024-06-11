using Moq;
using Xunit;
using Microsoft.EntityFrameworkCore;
using EventPlanner.Features;
using EventPlanner.Models;

public class GetEventByIdQueryHandlerTests
{
    private readonly ApplicationDbContext _context;
    private readonly GetEventByIdQueryHandler _handler;

    public GetEventByIdQueryHandlerTests()
    {
        var vOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        _context = new ApplicationDbContext(vOptions);

        _handler = new GetEventByIdQueryHandler(_context);
    }

    [Fact]
    public async Task Handle_GivenValidId_ShouldReturnEventDetail()
    {
        // Arrange
        var vEventId = Guid.NewGuid();
        var vEventEntity = new Event
        {
            Id = vEventId,
            Title = "Test Event",
            Description = "Test Description",
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddDays(1),
            Location = "Test Location"
        };

        _context.Events.Add(vEventEntity);
        await _context.SaveChangesAsync();

        var aQuery = new GetEventByIdQuery { Id = vEventId };

        // Act
        var vResult = await _handler.Handle(new GetEventByIdQuery { Id = vEventId }, CancellationToken.None);

        // Assert
        Assert.NotNull(vResult);
        Assert.Equal(vEventId, vResult.Id);
        Assert.Equal(vEventEntity.Title, vResult.Title);
        Assert.Equal(vEventEntity.Description, vResult.Description);
    }
}

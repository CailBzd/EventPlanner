using Xunit;
using EventPlanner.Models;
using Microsoft.EntityFrameworkCore;
using EventPlanner.Features;

namespace EventPlanner.Tests;

public class GetPollByIdQueryHandlerTests
{
    private readonly ApplicationDbContext _context;
    private readonly GetPollByIdQueryHandler _handler;

    public GetPollByIdQueryHandlerTests()
    {
        var vOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;
        _context = new ApplicationDbContext(vOptions);
        _handler = new GetPollByIdQueryHandler(_context);
    }

    [Fact]
    public async Task Handle_GivenValidId_ShouldReturnPollDetail()
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

        var aQuery = new GetPollByIdQuery { Id = vPollId };

        // Act
        var vResult = await _handler.Handle(aQuery, CancellationToken.None);

        // Assert
        Assert.NotNull(vResult);
        Assert.Equal(vPollId, vResult.Id);
        Assert.Equal("Test Poll", vResult.Question);
    }

    [Fact]
    public async Task Handle_GivenInvalidId_ShouldReturnNull()
    {
        // Arrange
        var aQuery = new GetPollByIdQuery { Id = Guid.NewGuid() };

        // Act
        var vResult = await _handler.Handle(aQuery, CancellationToken.None);

        // Assert
        Assert.Null(vResult);
    }
}

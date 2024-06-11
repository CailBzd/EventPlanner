using Xunit;
using EventPlanner.Models;
using Microsoft.EntityFrameworkCore;
using EventPlanner.Features;

namespace EventPlanner.Tests;

public class GetUserByIdQueryHandlerTests
{
    private readonly ApplicationDbContext _context;
    private readonly GetUserByIdQueryHandler _handler;

    public GetUserByIdQueryHandlerTests()
    {
        var vOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;
        _context = new ApplicationDbContext(vOptions);
        _handler = new GetUserByIdQueryHandler(_context);
    }

    [Fact]
    public async Task Handle_GivenValidId_ShouldReturnUserDetail()
    {
        // Arrange
        var vUserId = Guid.NewGuid();
        var vUserEntity = new User
        {
            Id = vUserId,
            UserName = "TestUser",
            Email = "test@example.com",
            PasswordHash = "hashedpassword"
        };

        await _context.Users.AddAsync(vUserEntity);
        await _context.SaveChangesAsync();

        var aQuery = new GetUserByIdQuery { Id = vUserId };

        // Act
        var vResult = await _handler.Handle(aQuery, CancellationToken.None);

        // Assert
        Assert.NotNull(vResult);
        Assert.Equal(vUserId, vResult.Id);
        Assert.Equal("TestUser", vResult.UserName);
    }

    [Fact]
    public async Task Handle_GivenInvalidId_ShouldReturnNull()
    {
        // Arrange
        var aQuery = new GetUserByIdQuery { Id = Guid.NewGuid() };

        // Act
        var vResult = await _handler.Handle(aQuery, CancellationToken.None);

        // Assert
        Assert.Null(vResult);
    }
}

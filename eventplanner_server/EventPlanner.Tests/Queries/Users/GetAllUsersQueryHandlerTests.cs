using Xunit;
using EventPlanner.Models;
using Microsoft.EntityFrameworkCore;
using EventPlanner.Features;

namespace EventPlanner.Tests;

public class GetAllUsersQueryHandlerTests
{
    private readonly ApplicationDbContext _context;
    private readonly GetAllUsersQueryHandler _handler;

    public GetAllUsersQueryHandlerTests()
    {
        var vOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;
        _context = new ApplicationDbContext(vOptions);
        _handler = new GetAllUsersQueryHandler(_context);
    }

    private async Task SeedDatabase()
    {
        var vUsers = new List<User>
        {
            new User { Id = Guid.NewGuid(), UserName = "User1", Email = "user1@example.com", PasswordHash = "hashedpassword1" },
            new User { Id = Guid.NewGuid(), UserName = "User2", Email = "user2@example.com", PasswordHash = "hashedpassword2" }
        };

        await _context.Users.AddRangeAsync(vUsers);
        await _context.SaveChangesAsync();
    }

    [Fact]
    public async Task Handle_ShouldReturnListOfUserDetails()
    {
        // Arrange
        await SeedDatabase();

        // Act
        var vResult = await _handler.Handle(new GetAllUsersQuery(), CancellationToken.None);

        // Assert
        Assert.NotNull(vResult);
        Assert.Equal(2, vResult.Count);
        Assert.Contains(vResult, e => e.UserName == "User1");
        Assert.Contains(vResult, e => e.UserName == "User2");
        Assert.Contains(vResult, e => e.Email == "user1@example.com");
        Assert.Contains(vResult, e => e.Email == "user2@example.com");
    }
}

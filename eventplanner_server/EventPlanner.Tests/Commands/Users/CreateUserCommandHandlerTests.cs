using Xunit;
using Microsoft.EntityFrameworkCore;
using EventPlanner.Features;

namespace EventPlanner.Tests;

public class CreateUserCommandHandlerTests
{
    private readonly ApplicationDbContext _context;
    private readonly CreateUserCommandHandler _handler;

    public CreateUserCommandHandlerTests()
    {
        var vOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;
        _context = new ApplicationDbContext(vOptions);
        _handler = new CreateUserCommandHandler(_context);
    }

    [Fact]
    public async Task Handle_GivenValidRequest_ShouldCreateUser()
    {
        // Arrange
        var aCommand = new CreateUserCommand
        {
            UserName = "TestUser",
            Email = "test@example.com",
            PasswordHash = "hashedpassword"
        };

        // Act
        var vResult = await _handler.Handle(aCommand, CancellationToken.None);

        // Assert
        Assert.NotEqual(Guid.Empty, vResult);
        Assert.NotNull(await _context.Users.FindAsync(vResult));
    }
}

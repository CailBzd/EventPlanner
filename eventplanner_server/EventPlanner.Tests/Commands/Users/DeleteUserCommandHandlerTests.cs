using Xunit;
using EventPlanner.Models;
using Microsoft.EntityFrameworkCore;
using EventPlanner.Features;

namespace EventPlanner.Tests;

public class DeleteUserCommandHandlerTests
{
    private readonly ApplicationDbContext _context;
    private readonly DeleteUserCommandHandler _handler;

    public DeleteUserCommandHandlerTests()
    {
        var vOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;
        _context = new ApplicationDbContext(vOptions);
        _handler = new DeleteUserCommandHandler(_context);
    }

    [Fact]
    public async Task Handle_GivenValidRequest_ShouldDeleteUser()
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

        var aCommand = new DeleteUserCommand { Id = vUserId };

        // Act
        var vResult = await _handler.Handle(aCommand, CancellationToken.None);

        // Assert
        Assert.True(vResult);
        var vDeletedUser = await _context.Users.FindAsync(vUserId);
        Assert.Null(vDeletedUser);
    }

    [Fact]
    public async Task Handle_GivenInvalidRequest_ShouldReturnFalse()
    {
        // Arrange
        var aCommand = new DeleteUserCommand { Id = Guid.NewGuid() };

        // Act
        var vResult = await _handler.Handle(aCommand, CancellationToken.None);

        // Assert
        Assert.False(vResult);
    }
}

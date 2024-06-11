using Xunit;
using EventPlanner.Models;
using Microsoft.EntityFrameworkCore;
using EventPlanner.Features;

namespace EventPlanner.Tests;

public class UpdateUserCommandHandlerTests
{
    private readonly ApplicationDbContext _context;
    private readonly UpdateUserCommandHandler _handler;

    public UpdateUserCommandHandlerTests()
    {
        var vOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;
        _context = new ApplicationDbContext(vOptions);
        _handler = new UpdateUserCommandHandler(_context);
    }

    [Fact]
    public async Task Handle_GivenValidRequest_ShouldUpdateUser()
    {
        // Arrange
        var vUserId = Guid.NewGuid();
        var vUserEntity = new User
        {
            Id = vUserId,
            UserName = "OriginalUser",
            Email = "original@example.com",
            PasswordHash = "originalpassword"
        };

        await _context.Users.AddAsync(vUserEntity);
        await _context.SaveChangesAsync();

        var aCommand = new UpdateUserCommand
        {
            Id = vUserId,
            UserName = "UpdatedUser",
            Email = "updated@example.com",
            PasswordHash = "updatedpassword"
        };

        // Act
        var vResult = await _handler.Handle(aCommand, CancellationToken.None);

        // Assert
        Assert.True(vResult);
        var vUpdatedUser = await _context.Users.FindAsync(vUserId);
        Assert.Equal("UpdatedUser", vUpdatedUser.UserName);
    }

    [Fact]
    public async Task Handle_GivenInvalidRequest_ShouldReturnFalse()
    {
        // Arrange
        var aCommand = new UpdateUserCommand
        {
            Id = Guid.NewGuid(),
            UserName = "UpdatedUser",
            Email = "updated@example.com",
            PasswordHash = "updatedpassword"
        };

        // Act
        var vResult = await _handler.Handle(aCommand, CancellationToken.None);

        // Assert
        Assert.False(vResult);
    }
}

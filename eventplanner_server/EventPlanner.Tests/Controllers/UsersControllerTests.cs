using Microsoft.AspNetCore.Identity;
using Moq;
using Xunit;
using EventPlanner.Controllers;
using Microsoft.AspNetCore.Mvc;
using EventPlanner.Models;
using EventPlanner.Features;

namespace EventPlanner.Tests;
public class UsersControllerTests
{
    private readonly Mock<UserManager<User>> _userManagerMock;
    private readonly UsersController _controller;

    public UsersControllerTests()
    {
        var vStore = new Mock<IUserStore<User>>();
        _userManagerMock = new Mock<UserManager<User>>(vStore.Object, null, null, null, null, null, null, null, null);
        _controller = new UsersController(_userManagerMock.Object);
    }

    [Fact]
    public async Task GetUserById_ReturnsOkResult_WhenUserIsFound()
    {
        // Arrange
        var vUserId = Guid.NewGuid();
        var vUser = new User { Id = vUserId };
        _userManagerMock.Setup(um => um.FindByIdAsync(vUserId.ToString())).ReturnsAsync(vUser);

        // Act
        var vResult = await _controller.GetUserById(vUserId);

        // Assert
        var vOkResult = Assert.IsType<OkObjectResult>(vResult);
        Assert.Equal(vUser, vOkResult.Value);
    }

    [Fact]
    public async Task GetUserById_ReturnsNotFound_WhenUserIsNotFound()
    {
        // Arrange
        var vUserId = Guid.NewGuid();
        _userManagerMock.Setup(um => um.FindByIdAsync(vUserId.ToString())).ReturnsAsync((User)null);

        // Act
        var vResult = await _controller.GetUserById(vUserId);

        // Assert
        Assert.IsType<NotFoundResult>(vResult);
    }

    [Fact]
    public void GetAllUsers_ReturnsOkResult_WithListOfUsers()
    {
        // Arrange
        var vUsers = new List<User>
            {
                new User { Id = Guid.NewGuid() },
                new User { Id = Guid.NewGuid() }
            };
        _userManagerMock.Setup(um => um.Users).Returns(vUsers.AsQueryable());

        // Act
        var vResult = _controller.GetAllUsers();

        // Assert
        var vOkResult = Assert.IsType<OkObjectResult>(vResult);
        Assert.Equal(vUsers, vOkResult.Value);
    }

    [Fact]
    public async Task UpdateUser_ReturnsNoContent_WhenUpdateIsSuccessful()
    {
        // Arrange
        var vUserId = Guid.NewGuid();
        var vUser = new User { Id = vUserId };
        var command = new UpdateUserCommand { UserName = "newusername", Email = "newemail@example.com" };

        _userManagerMock.Setup(um => um.FindByIdAsync(vUserId.ToString())).ReturnsAsync(vUser);
        _userManagerMock.Setup(um => um.UpdateAsync(vUser)).ReturnsAsync(IdentityResult.Success);

        // Act
        var vResult = await _controller.UpdateUser(vUserId, command);

        // Assert
        Assert.IsType<NoContentResult>(vResult);
    }

    [Fact]
    public async Task UpdateUser_ReturnsNotFound_WhenUserIsNotFound()
    {
        // Arrange
        var vUserId = Guid.NewGuid();
        var vCommand = new UpdateUserCommand { UserName = "newusername", Email = "newemail@example.com" };

        _userManagerMock.Setup(um => um.FindByIdAsync(vUserId.ToString())).ReturnsAsync((User)null);

        // Act
        var vResult = await _controller.UpdateUser(vUserId, vCommand);

        // Assert
        Assert.IsType<NotFoundResult>(vResult);
    }
}

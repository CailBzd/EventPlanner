using Moq;
using EventPlanner.Controllers;
using EventPlanner.Features;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using MediatR;

namespace EventPlanner.Tests;

public class AuthControllerTests
{
    private readonly Mock<IMediator> _mediatorMock;
    private readonly AuthController _controller;

    public AuthControllerTests()
    {
        _mediatorMock = new Mock<IMediator>();
        _controller = new AuthController(_mediatorMock.Object);
    }

    [Fact]
    public async Task Register_ReturnsOkResult_WhenUserIsCreated()
    {
        // Arrange
        var vCommand = new CreateUserCommand { UserName = "testuser", Email = "testuser@example.com", PasswordHash = "Test@123" };
        _mediatorMock.Setup(m => m.Send(It.IsAny<CreateUserCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(Guid.NewGuid());

        // Act
        var vResult = await _controller.Register(vCommand);

        // Assert
        var vOkResult = Assert.IsType<OkObjectResult>(vResult);
        Assert.NotNull(vOkResult.Value.GetType().GetProperty("Id").GetValue(vOkResult.Value, null));
    }

    [Fact]
    public async Task Register_ReturnsBadRequest_WhenModelStateIsInvalid()
    {
        // Arrange
        _controller.ModelState.AddModelError("error", "some error");
        var command = new CreateUserCommand { UserName = "testuser", Email = "testuser@example.com", PasswordHash = "Test@123" };

        // Act
        var vResult = await _controller.Register(command);

        // Assert
        var vBadRequestResult = Assert.IsType<BadRequestObjectResult>(vResult);
        Assert.IsType<SerializableError>(vBadRequestResult.Value);
    }

    [Fact]
    public async Task Login_ReturnsOkResult_WithToken_WhenLoginIsSuccessful()
    {
        // Arrange
        var vCommand = new LoginCommand { Username = "testuser", Password = "Test@123" };
        var vToken = "fake-jwt-token";
        _mediatorMock.Setup(m => m.Send(It.IsAny<LoginCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(vToken);

        // Act
        var vResult = await _controller.Login(vCommand);

        // Assert
        var vOkResult = Assert.IsType<OkObjectResult>(vResult);
        var vTokenResult = Assert.IsType<string>(vOkResult.Value.GetType().GetProperty("Token").GetValue(vOkResult.Value, null));
        Assert.Equal(vToken, vTokenResult);
    }

    [Fact]
    public async Task Login_ReturnsUnauthorized_WhenLoginFails()
    {
        // Arrange
        var vCommand = new LoginCommand { Username = "testuser", Password = "Test@123" };
        _mediatorMock.Setup(m => m.Send(It.IsAny<LoginCommand>(), It.IsAny<CancellationToken>())).Throws(new UnauthorizedAccessException("Invalid username or password"));

        // Act
        var vResult = await _controller.Login(vCommand);

        // Assert
        var vUnauthorizedResult = Assert.IsType<UnauthorizedObjectResult>(vResult);
        Assert.Equal("Invalid username or password", vUnauthorizedResult.Value.GetType().GetProperty("Message").GetValue(vUnauthorizedResult.Value, null));
    }

    [Fact]
    public async Task Logout_ReturnsOkResult()
    {
        // Arrange
        var vCommand = new LogoutCommand();
        _mediatorMock.Setup(m => m.Send(It.IsAny<LogoutCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(Unit.Value);

        // Act
        var vResult = await _controller.Logout(vCommand);

        // Assert
        var vOkResult = Assert.IsType<OkObjectResult>(vResult);
        Assert.Equal("User logged out successfully", vOkResult.Value.GetType().GetProperty("Message").GetValue(vOkResult.Value, null));
    }
}


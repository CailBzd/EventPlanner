using MediatR;
using Moq;
using Xunit;
using EventPlanner.Controllers;
using Microsoft.AspNetCore.Mvc;
using EventPlanner.Features;

namespace EventPlanner.Tests;
public class NotificationsControllerTests
{
    private readonly Mock<IMediator> _mediatorMock;
    private readonly NotificationsController _controller;

    public NotificationsControllerTests()
    {
        _mediatorMock = new Mock<IMediator>();
        _controller = new NotificationsController(_mediatorMock.Object);
    }

    [Fact]
    public async Task CreateNotification_ReturnsOkResult_WithNotificationId()
    {
        // Arrange
        var command = new CreateNotificationCommand { Message = "Test Notification" };
        var notificationId = Guid.NewGuid();
        _mediatorMock.Setup(m => m.Send(It.IsAny<CreateNotificationCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(notificationId);

        // Act
        var result = await _controller.CreateNotification(command);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(notificationId, okResult.Value.GetType().GetProperty("Id").GetValue(okResult.Value, null));
    }

    [Fact]
    public async Task GetNotificationById_ReturnsOkResult_WhenNotificationIsFound()
    {
        // Arrange
        var notificationId = Guid.NewGuid();
        var notificationDetail = new NotificationDetailDto { Id = notificationId };
        _mediatorMock.Setup(m => m.Send(It.IsAny<GetNotificationByIdQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(notificationDetail);

        // Act
        var result = await _controller.GetNotificationById(notificationId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(notificationDetail, okResult.Value);
    }

    [Fact]
    public async Task GetNotificationById_ReturnsNotFound_WhenNotificationIsNotFound()
    {
        // Arrange
        var notificationId = Guid.NewGuid();
        _mediatorMock.Setup(m => m.Send(It.IsAny<GetNotificationByIdQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync((NotificationDetailDto)null);

        // Act
        var result = await _controller.GetNotificationById(notificationId);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task UpdateNotification_ReturnsNoContent_WhenUpdateIsSuccessful()
    {
        // Arrange
        var notificationId = Guid.NewGuid();
        var command = new UpdateNotificationCommand { Id = notificationId, Message = "Updated Notification" };
        _mediatorMock.Setup(m => m.Send(It.IsAny<UpdateNotificationCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(true);

        // Act
        var result = await _controller.UpdateNotification(notificationId, command);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task UpdateNotification_ReturnsNotFound_WhenNotificationIsNotFound()
    {
        // Arrange
        var notificationId = Guid.NewGuid();
        var command = new UpdateNotificationCommand { Id = notificationId, Message = "Updated Notification" };
        _mediatorMock.Setup(m => m.Send(It.IsAny<UpdateNotificationCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(false);

        // Act
        var result = await _controller.UpdateNotification(notificationId, command);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task DeleteNotification_ReturnsNoContent_WhenDeleteIsSuccessful()
    {
        // Arrange
        var notificationId = Guid.NewGuid();
        _mediatorMock.Setup(m => m.Send(It.IsAny<DeleteNotificationCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(true);

        // Act
        var result = await _controller.DeleteNotification(notificationId);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task DeleteNotification_ReturnsNotFound_WhenNotificationIsNotFound()
    {
        // Arrange
        var notificationId = Guid.NewGuid();
        _mediatorMock.Setup(m => m.Send(It.IsAny<DeleteNotificationCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(false);

        // Act
        var result = await _controller.DeleteNotification(notificationId);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }
}
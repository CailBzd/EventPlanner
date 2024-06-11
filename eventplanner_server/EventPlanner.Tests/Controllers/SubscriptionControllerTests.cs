using MediatR;
using Moq;
using Xunit;
using EventPlanner.Controllers;
using Microsoft.AspNetCore.Mvc;
using EventPlanner.Features;

namespace EventPlanner.Tests;
public class SubscriptionControllerTests
{
    private readonly Mock<IMediator> _mediatorMock;
    private readonly SubscriptionController _controller;

    public SubscriptionControllerTests()
    {
        _mediatorMock = new Mock<IMediator>();
        _controller = new SubscriptionController(_mediatorMock.Object);
    }

    [Fact]
    public async Task Subscribe_ReturnsOkResult_WithSubscriptionId()
    {
        // Arrange
        var command = new CreateSubscriptionCommand { Plan = Guid.NewGuid().ToString(), UserId = Guid.NewGuid() };
        var subscriptionId = Guid.NewGuid();
        _mediatorMock.Setup(m => m.Send(It.IsAny<CreateSubscriptionCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(subscriptionId);

        // Act
        var result = await _controller.Subscribe(command);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(subscriptionId, okResult.Value.GetType().GetProperty("Id").GetValue(okResult.Value, null));
    }

    [Fact]
    public async Task Unsubscribe_ReturnsNoContent_WhenUnsubscribeIsSuccessful()
    {
        // Arrange
        var command = new DeleteSubscriptionCommand { Id = Guid.NewGuid() };
        _mediatorMock.Setup(m => m.Send(It.IsAny<DeleteSubscriptionCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(true);

        // Act
        var result = await _controller.Unsubscribe(command);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task Unsubscribe_ReturnsBadRequest_WhenUnsubscribeFails()
    {
        // Arrange
        var command = new DeleteSubscriptionCommand { Id = Guid.NewGuid() };
        _mediatorMock.Setup(m => m.Send(It.IsAny<DeleteSubscriptionCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(false);

        // Act
        var result = await _controller.Unsubscribe(command);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task GetSubscriptionById_ReturnsOkResult_WhenSubscriptionIsFound()
    {
        // Arrange
        var subscriptionId = Guid.NewGuid();
        var subscriptionDetail = new SubscriptionDetailDto { Id = subscriptionId };
        _mediatorMock.Setup(m => m.Send(It.IsAny<GetSubscriptionByIdQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(subscriptionDetail);

        // Act
        var result = await _controller.GetSubscriptionById(subscriptionId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(subscriptionDetail, okResult.Value);
    }

    [Fact]
    public async Task GetSubscriptionById_ReturnsNotFound_WhenSubscriptionIsNotFound()
    {
        // Arrange
        var subscriptionId = Guid.NewGuid();
        _mediatorMock.Setup(m => m.Send(It.IsAny<GetSubscriptionByIdQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync((SubscriptionDetailDto)null);

        // Act
        var result = await _controller.GetSubscriptionById(subscriptionId);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }
}
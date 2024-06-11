using MediatR;
using Moq;
using Xunit;
using EventPlanner.Controllers;
using Microsoft.AspNetCore.Mvc;
using EventPlanner.Features;

namespace EventPlanner.Tests;
public class PollsControllerTests
{
    private readonly Mock<IMediator> _mediatorMock;
    private readonly PollsController _controller;

    public PollsControllerTests()
    {
        _mediatorMock = new Mock<IMediator>();
        _controller = new PollsController(_mediatorMock.Object);
    }

    [Fact]
    public async Task CreatePoll_ReturnsOkResult_WithPollId()
    {
        // Arrange
        var command = new CreatePollCommand { Question = "Test Poll?" };
        var pollId = Guid.NewGuid();
        _mediatorMock.Setup(m => m.Send(It.IsAny<CreatePollCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(pollId);

        // Act
        var result = await _controller.CreatePoll(command);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(pollId, okResult.Value.GetType().GetProperty("Id").GetValue(okResult.Value, null));
    }

    [Fact]
    public async Task GetPollById_ReturnsOkResult_WhenPollIsFound()
    {
        // Arrange
        var pollId = Guid.NewGuid();
        var pollDetail = new PollDetailDto { Id = pollId };
        _mediatorMock.Setup(m => m.Send(It.IsAny<GetPollByIdQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(pollDetail);

        // Act
        var result = await _controller.GetPollById(pollId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(pollDetail, okResult.Value);
    }

    [Fact]
    public async Task GetPollById_ReturnsNotFound_WhenPollIsNotFound()
    {
        // Arrange
        var pollId = Guid.NewGuid();
        _mediatorMock.Setup(m => m.Send(It.IsAny<GetPollByIdQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync((PollDetailDto)null);

        // Act
        var result = await _controller.GetPollById(pollId);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task UpdatePoll_ReturnsNoContent_WhenUpdateIsSuccessful()
    {
        // Arrange
        var pollId = Guid.NewGuid();
        var command = new UpdatePollCommand { Id = pollId, Question = "Updated Poll?" };
        _mediatorMock.Setup(m => m.Send(It.IsAny<UpdatePollCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(true);

        // Act
        var result = await _controller.UpdatePoll(pollId, command);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task UpdatePoll_ReturnsNotFound_WhenPollIsNotFound()
    {
        // Arrange
        var pollId = Guid.NewGuid();
        var command = new UpdatePollCommand { Id = pollId, Question = "Updated Poll?" };
        _mediatorMock.Setup(m => m.Send(It.IsAny<UpdatePollCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(false);

        // Act
        var result = await _controller.UpdatePoll(pollId, command);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task DeletePoll_ReturnsNoContent_WhenDeleteIsSuccessful()
    {
        // Arrange
        var pollId = Guid.NewGuid();
        _mediatorMock.Setup(m => m.Send(It.IsAny<DeletePollCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(true);

        // Act
        var result = await _controller.DeletePoll(pollId);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task DeletePoll_ReturnsNotFound_WhenPollIsNotFound()
    {
        // Arrange
        var pollId = Guid.NewGuid();
        _mediatorMock.Setup(m => m.Send(It.IsAny<DeletePollCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(false);

        // Act
        var result = await _controller.DeletePoll(pollId);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }
}
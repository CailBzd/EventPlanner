using MediatR;
using Moq;
using Xunit;
using EventPlanner.Controllers;
using Microsoft.AspNetCore.Mvc;
using EventPlanner.Features;

namespace EventPlanner.Tests;

public class EventsControllerTests
{
    private readonly Mock<IMediator> _mediatorMock;
    private readonly EventsController _controller;

    public EventsControllerTests()
    {
        _mediatorMock = new Mock<IMediator>();
        _controller = new EventsController(_mediatorMock.Object);
    }

    [Fact]
    public async Task CreateEvent_ReturnsOkResult_WithEventId()
    {
        // Arrange
        var vCommand = new CreateEventCommand { Title = "Test Event" };
        var vEventId = Guid.NewGuid();
        _mediatorMock.Setup(m => m.Send(It.IsAny<CreateEventCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(vEventId);

        // Act
        var vResult = await _controller.CreateEvent(vCommand);

        // Assert
        var vOkResult = Assert.IsType<OkObjectResult>(vResult);
        Assert.Equal(vEventId, vOkResult.Value.GetType().GetProperty("Id").GetValue(vOkResult.Value, null));
    }

    [Fact]
    public async Task GetEventById_ReturnsOkResult_WhenEventIsFound()
    {
        // Arrange
        var vEventId = Guid.NewGuid();
        var vEventDetail = new EventDetailDto { Id = vEventId };
        _mediatorMock.Setup(m => m.Send(It.IsAny<GetEventByIdQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(vEventDetail);

        // Act
        var vResult = await _controller.GetEventById(vEventId);

        // Assert
        var vOkResult = Assert.IsType<OkObjectResult>(vResult);
        Assert.Equal(vEventDetail, vOkResult.Value);
    }

    [Fact]
    public async Task GetEventById_ReturnsNotFound_WhenEventIsNotFound()
    {
        // Arrange
        var vEventId = Guid.NewGuid();
        _mediatorMock.Setup(m => m.Send(It.IsAny<GetEventByIdQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync((EventDetailDto)null);

        // Act
        var vResult = await _controller.GetEventById(vEventId);

        // Assert
        Assert.IsType<NotFoundResult>(vResult);
    }

    [Fact]
    public async Task UpdateEvent_ReturnsNoContent_WhenUpdateIsSuccessful()
    {
        // Arrange
        var vEventId = Guid.NewGuid();
        var vCommand = new UpdateEventCommand { Id = vEventId, Title = "Updated Event" };
        _mediatorMock.Setup(m => m.Send(It.IsAny<UpdateEventCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(true);

        // Act
        var vResult = await _controller.UpdateEvent(vEventId, vCommand);

        // Assert
        Assert.IsType<NoContentResult>(vResult);
    }

    [Fact]
    public async Task UpdateEvent_ReturnsNotFound_WhenEventIsNotFound()
    {
        // Arrange
        var vEventId = Guid.NewGuid();
        var vCommand = new UpdateEventCommand { Id = vEventId, Title = "Updated Event" };
        _mediatorMock.Setup(m => m.Send(It.IsAny<UpdateEventCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(false);

        // Act
        var vResult = await _controller.UpdateEvent(vEventId, vCommand);

        // Assert
        Assert.IsType<NotFoundResult>(vResult);
    }

    [Fact]
    public async Task DeleteEvent_ReturnsNoContent_WhenDeleteIsSuccessful()
    {
        // Arrange
        var vEventId = Guid.NewGuid();
        _mediatorMock.Setup(m => m.Send(It.IsAny<DeleteEventCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(true);

        // Act
        var vResult = await _controller.DeleteEvent(vEventId);

        // Assert
        Assert.IsType<NoContentResult>(vResult);
    }

    [Fact]
    public async Task DeleteEvent_ReturnsNotFound_WhenEventIsNotFound()
    {
        // Arrange
        var vEventId = Guid.NewGuid();
        _mediatorMock.Setup(m => m.Send(It.IsAny<DeleteEventCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(false);

        // Act
        var vResult = await _controller.DeleteEvent(vEventId);

        // Assert
        Assert.IsType<NotFoundResult>(vResult);
    }
}

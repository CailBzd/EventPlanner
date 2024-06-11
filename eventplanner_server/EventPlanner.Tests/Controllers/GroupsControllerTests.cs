using MediatR;
using Moq;
using Xunit;
using EventPlanner.Controllers;
using Microsoft.AspNetCore.Mvc;
using EventPlanner.Features;

namespace EventPlanner.Tests;
public class GroupsControllerTests
{
    private readonly Mock<IMediator> _mediatorMock;
    private readonly GroupsController _controller;

    public GroupsControllerTests()
    {
        _mediatorMock = new Mock<IMediator>();
        _controller = new GroupsController(_mediatorMock.Object);
    }

    [Fact]
    public async Task CreateGroup_ReturnsOkResult_WithGroupId()
    {
        // Arrange
        var command = new CreateGroupCommand { Name = "Test Group" };
        var groupId = Guid.NewGuid();
        _mediatorMock.Setup(m => m.Send(It.IsAny<CreateGroupCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(groupId);

        // Act
        var result = await _controller.CreateGroup(command);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(groupId, okResult.Value.GetType().GetProperty("Id").GetValue(okResult.Value, null));
    }

    [Fact]
    public async Task GetGroupById_ReturnsOkResult_WhenGroupIsFound()
    {
        // Arrange
        var groupId = Guid.NewGuid();
        var groupDetail = new GroupDetailDto { Id = groupId };
        _mediatorMock.Setup(m => m.Send(It.IsAny<GetGroupByIdQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(groupDetail);

        // Act
        var result = await _controller.GetGroupById(groupId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(groupDetail, okResult.Value);
    }

    [Fact]
    public async Task GetGroupById_ReturnsNotFound_WhenGroupIsNotFound()
    {
        // Arrange
        var groupId = Guid.NewGuid();
        _mediatorMock.Setup(m => m.Send(It.IsAny<GetGroupByIdQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync((GroupDetailDto)null);

        // Act
        var result = await _controller.GetGroupById(groupId);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task UpdateGroup_ReturnsNoContent_WhenUpdateIsSuccessful()
    {
        // Arrange
        var groupId = Guid.NewGuid();
        var command = new UpdateGroupCommand { Id = groupId, Name = "Updated Group" };
        _mediatorMock.Setup(m => m.Send(It.IsAny<UpdateGroupCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(true);

        // Act
        var result = await _controller.UpdateGroup(groupId, command);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task UpdateGroup_ReturnsNotFound_WhenGroupIsNotFound()
    {
        // Arrange
        var groupId = Guid.NewGuid();
        var command = new UpdateGroupCommand { Id = groupId, Name = "Updated Group" };
        _mediatorMock.Setup(m => m.Send(It.IsAny<UpdateGroupCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(false);

        // Act
        var result = await _controller.UpdateGroup(groupId, command);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task DeleteGroup_ReturnsNoContent_WhenDeleteIsSuccessful()
    {
        // Arrange
        var groupId = Guid.NewGuid();
        _mediatorMock.Setup(m => m.Send(It.IsAny<DeleteGroupCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(true);

        // Act
        var result = await _controller.DeleteGroup(groupId);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task DeleteGroup_ReturnsNotFound_WhenGroupIsNotFound()
    {
        // Arrange
        var groupId = Guid.NewGuid();
        _mediatorMock.Setup(m => m.Send(It.IsAny<DeleteGroupCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(false);

        // Act
        var result = await _controller.DeleteGroup(groupId);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }
}

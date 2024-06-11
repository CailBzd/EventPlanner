using Microsoft.AspNetCore.Mvc;
using MediatR;
using EventPlanner.Features;

namespace EventPlanner.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GroupsController : ControllerBase
{
    private readonly IMediator _mediator;

    public GroupsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateGroup([FromBody] CreateGroupCommand command)
    {
        var groupId = await _mediator.Send(command);
        return Ok(new { Id = groupId });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetGroupById(Guid id)
    {
        var groupDetail = await _mediator.Send(new GetGroupByIdQuery { Id = id });
        if (groupDetail == null)
        {
            return NotFound();
        }
        return Ok(groupDetail);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllGroups()
    {
        var groups = await _mediator.Send(new GetAllGroupsQuery());
        return Ok(groups);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateGroup(Guid id, [FromBody] UpdateGroupCommand command)
    {
        command.Id = id;
        var result = await _mediator.Send(command);
        return result ? NoContent() : NotFound();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteGroup(Guid id)
    {
        var result = await _mediator.Send(new DeleteGroupCommand { Id = id });
        return result ? NoContent() : NotFound();
    }
}


using Microsoft.AspNetCore.Mvc;
using MediatR;
using EventPlanner.Features;

namespace EventPlanner.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PollsController : ControllerBase
{
    private readonly IMediator _mediator;

    public PollsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreatePoll([FromBody] CreatePollCommand command)
    {
        var pollId = await _mediator.Send(command);
        return Ok(new { Id = pollId });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPollById(Guid id)
    {
        var pollDetail = await _mediator.Send(new GetPollByIdQuery { Id = id });
        if (pollDetail == null)
        {
            return NotFound();
        }
        return Ok(pollDetail);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllPolls()
    {
        var polls = await _mediator.Send(new GetAllPollsQuery());
        return Ok(polls);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePoll(Guid id, [FromBody] UpdatePollCommand command)
    {
        command.Id = id;
        var result = await _mediator.Send(command);
        return result ? NoContent() : NotFound();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePoll(Guid id)
    {
        var result = await _mediator.Send(new DeletePollCommand { Id = id });
        return result ? NoContent() : NotFound();
    }
}

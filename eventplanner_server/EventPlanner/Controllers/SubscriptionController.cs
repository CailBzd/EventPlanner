using Microsoft.AspNetCore.Mvc;
using MediatR;
using EventPlanner.Features;

namespace EventPlanner.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SubscriptionController : ControllerBase
{
    private readonly IMediator _mediator;

    public SubscriptionController(IMediator aMediator)
    {
        _mediator = aMediator;
    }

    [HttpPost("subscribe")]
    public async Task<IActionResult> Subscribe([FromBody] CreateSubscriptionCommand aCommand)
    {
        var vSubscriptionId = await _mediator.Send(aCommand);
        return Ok(new { Id = vSubscriptionId });
    }

    [HttpPost("unsubscribe")]
    public async Task<IActionResult> Unsubscribe([FromBody] DeleteSubscriptionCommand command)
    {
        var vResult = await _mediator.Send(command);
        return vResult ? NoContent() : BadRequest("Failed to unsubscribe");
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetSubscriptionById(Guid aId)
    {
        var vSubscriptionDetail = await _mediator.Send(new GetSubscriptionByIdQuery { Id = aId });
        if (vSubscriptionDetail == null)
        {
            return NotFound();
        }
        return Ok(vSubscriptionDetail);
    }
}


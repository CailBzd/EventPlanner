using Microsoft.AspNetCore.Mvc;
using MediatR;
using EventPlanner.Features;

namespace EventPlanner.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EventsController : ControllerBase
{
    private readonly IMediator _mediator;

    public EventsController(IMediator aMediator)
    {
        _mediator = aMediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateEvent([FromBody] CreateEventCommand aCommand)
    {
        var vEventId = await _mediator.Send(aCommand);
        return Ok(new { Id = vEventId });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetEventById(Guid aId)
    {
        var vEventDetail = await _mediator.Send(new GetEventByIdQuery { Id = aId });
        if (vEventDetail == null)
        {
            return NotFound();
        }
        return Ok(vEventDetail);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllEvents()
    {
        var vEvents = await _mediator.Send(new GetAllEventsQuery());
        return Ok(vEvents);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateEvent(Guid aId, [FromBody] UpdateEventCommand aCommand)
    {
        aCommand.Id = aId;
        var vResult = await _mediator.Send(aCommand);
        return vResult ? NoContent() : NotFound();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEvent(Guid aId)
    {
        var vResult = await _mediator.Send(new DeleteEventCommand { Id = aId });
        return vResult ? NoContent() : NotFound();
    }
}

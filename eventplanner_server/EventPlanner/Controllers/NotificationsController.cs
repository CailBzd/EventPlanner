using Microsoft.AspNetCore.Mvc;
using MediatR;
using EventPlanner.Features;

namespace EventPlanner.Controllers;

[Route("api/[controller]")]
[ApiController]
public class NotificationsController : ControllerBase
{
    private readonly IMediator _mediator;

    public NotificationsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateNotification([FromBody] CreateNotificationCommand command)
    {
        var notificationId = await _mediator.Send(command);
        return Ok(new { Id = notificationId });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetNotificationById(Guid id)
    {
        var notificationDetail = await _mediator.Send(new GetNotificationByIdQuery { Id = id });
        if (notificationDetail == null)
        {
            return NotFound();
        }
        return Ok(notificationDetail);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllNotifications()
    {
        var notifications = await _mediator.Send(new GetAllNotificationsQuery());
        return Ok(notifications);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateNotification(Guid id, [FromBody] UpdateNotificationCommand command)
    {
        command.Id = id;
        var result = await _mediator.Send(command);
        return result ? NoContent() : NotFound();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteNotification(Guid id)
    {
        var result = await _mediator.Send(new DeleteNotificationCommand { Id = id });
        return result ? NoContent() : NotFound();
    }
}


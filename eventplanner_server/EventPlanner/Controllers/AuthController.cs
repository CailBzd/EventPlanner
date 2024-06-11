using Microsoft.AspNetCore.Mvc;
using MediatR;
using EventPlanner.Features;

namespace EventPlanner.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator aMediator)
    {
        _mediator = aMediator;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] CreateUserCommand aCommand)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var vUserId = await _mediator.Send(aCommand);
        return Ok(new { Id = vUserId });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginCommand aCommand)
    {
        try
        {
            var vToken = await _mediator.Send(aCommand);
            return Ok(new { Token = vToken });
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { Message = ex.Message });
        }
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout([FromBody] LogoutCommand aCommand)
    {
        await _mediator.Send(aCommand);
        return Ok(new { Message = "User logged out successfully" });
    }
}

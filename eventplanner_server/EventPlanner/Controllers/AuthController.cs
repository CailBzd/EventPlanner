using Microsoft.AspNetCore.Mvc;
using MediatR;
using EventPlanner.Features;
using Microsoft.AspNetCore.Authorization;

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
            var vResult = await _mediator.Send(aCommand);
            return Ok(vResult);
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { Message = ex.Message });
        }
    }

    [HttpPost("logout")]
    [Authorize]
    public async Task<IActionResult> Logout([FromBody] LogoutCommand aCommand)
    {
        await _mediator.Send(aCommand);
        return Ok(new { Message = "User logged out successfully" });
    }

    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordCommand command)
    {
        var result = await _mediator.Send(command);
        if (result)
        {
            return Ok(new { Message = "Reset password link has been sent to your email." });
        }
        return BadRequest(new { Message = "User not found" });
    }

    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordCommand command)
    {
        var result = await _mediator.Send(command);
        if (result)
        {
            return Ok(new { Message = "Password has been reset successfully." });
        }
        return BadRequest(new { Message = "Invalid request" });
    }

}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using EventPlanner.Models;
using EventPlanner.Features;
using Microsoft.AspNetCore.Authorization;
using MediatR;

namespace EventPlanner.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(Guid id)
    {
        var query = new GetUserByIdQuery { Id = id };
        var user = await _mediator.Send(query);
        if (user != null)
        {
            return Ok(user);
        }

        return NotFound();
    }

  
    [HttpPut("update")]
    public async Task<IActionResult> UpdateUser([FromBody] UpdateUserCommand aCommand)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _mediator.Send(aCommand);
        if (result)
        {
            return NoContent();
        }

        return BadRequest("Could not update user");
    }
}

using MediatR;

namespace EventPlanner.Features;

public class LoginCommand : IRequest<LoginResult>
{
    public string Username { get; set; }
    public string Password { get; set; }
}

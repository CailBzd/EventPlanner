using MediatR;

namespace EventPlanner.Features;
public class CreateUserCommand : IRequest<Guid>
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
}

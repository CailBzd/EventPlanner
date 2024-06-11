using MediatR;

namespace EventPlanner.Features;
public class UpdateUserCommand : IRequest<bool>
{
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
}

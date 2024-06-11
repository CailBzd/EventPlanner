using MediatR;

namespace EventPlanner.Features;
public class DeleteUserCommand : IRequest<bool>
{
    public Guid Id { get; set; }
}

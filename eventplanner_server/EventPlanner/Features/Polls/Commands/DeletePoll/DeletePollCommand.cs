using MediatR;

namespace EventPlanner.Features;
public class DeletePollCommand : IRequest<bool>
{
    public Guid Id { get; set; }
}

using MediatR;

namespace EventPlanner.Features;
public class UpdatePollCommand : IRequest<bool>
{
    public Guid Id { get; set; }
    public string Question { get; set; }
}

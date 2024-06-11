using MediatR;

namespace EventPlanner.Features;
public class CreatePollCommand : IRequest<Guid>
{
    public string Question { get; set; }
}

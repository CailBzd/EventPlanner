using MediatR;

namespace EventPlanner.Features;
public class DeleteEventCommand : IRequest<bool>
{
    public Guid Id { get; set; }
}
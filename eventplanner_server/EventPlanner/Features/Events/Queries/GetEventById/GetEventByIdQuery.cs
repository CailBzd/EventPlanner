using MediatR;

namespace EventPlanner.Features;
public class GetEventByIdQuery : IRequest<EventDetailDto>
{
    public Guid Id { get; set; }
}

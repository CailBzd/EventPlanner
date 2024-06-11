using MediatR;

namespace EventPlanner.Features;
public class GetAllEventsQuery : IRequest<List<EventDetailDto>>
{
}

using MediatR;

namespace EventPlanner.Features;
public class GetAllPollsQuery : IRequest<List<PollDetailDto>>
{
}

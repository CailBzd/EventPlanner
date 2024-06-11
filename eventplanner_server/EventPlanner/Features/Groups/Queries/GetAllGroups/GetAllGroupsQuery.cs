using MediatR;

namespace EventPlanner.Features;
public class GetAllGroupsQuery : IRequest<List<GroupDetailDto>>
{
}
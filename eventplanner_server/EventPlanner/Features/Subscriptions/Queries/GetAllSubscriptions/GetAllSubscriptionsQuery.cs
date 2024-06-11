using MediatR;

namespace EventPlanner.Features;
public class GetAllSubscriptionsQuery : IRequest<List<SubscriptionDetailDto>>
{
}

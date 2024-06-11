using MediatR;

namespace EventPlanner.Features;
public class GetSubscriptionByIdQuery : IRequest<SubscriptionDetailDto>
{
    public Guid Id { get; set; }
}

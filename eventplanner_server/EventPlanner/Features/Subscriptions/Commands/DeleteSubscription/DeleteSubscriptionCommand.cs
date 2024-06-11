using MediatR;

namespace EventPlanner.Features;
public class DeleteSubscriptionCommand : IRequest<bool>
{
    public Guid Id { get; set; }
}

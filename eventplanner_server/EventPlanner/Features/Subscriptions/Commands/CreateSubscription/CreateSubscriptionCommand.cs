using MediatR;

namespace EventPlanner.Features;
public class CreateSubscriptionCommand : IRequest<Guid>
{
    public Guid UserId { get; set; }
    public string Plan { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}

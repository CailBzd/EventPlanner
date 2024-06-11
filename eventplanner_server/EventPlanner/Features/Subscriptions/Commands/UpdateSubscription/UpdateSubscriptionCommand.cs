using MediatR;

namespace EventPlanner.Features;
public class UpdateSubscriptionCommand : IRequest<bool>
{
    public Guid Id { get; set; }
    public string Plan { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}

using MediatR;

namespace EventPlanner.Features;
public class DeleteNotificationCommand : IRequest<bool>
{
    public Guid Id { get; set; }
}
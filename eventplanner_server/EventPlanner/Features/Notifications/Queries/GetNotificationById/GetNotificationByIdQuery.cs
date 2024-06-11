using MediatR;

namespace EventPlanner.Features;
public class GetNotificationByIdQuery : IRequest<NotificationDetailDto>
{
    public Guid Id { get; set; }
}

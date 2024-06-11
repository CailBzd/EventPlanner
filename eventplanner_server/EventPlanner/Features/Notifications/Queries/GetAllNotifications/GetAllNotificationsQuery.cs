using MediatR;

namespace EventPlanner.Features;
public class GetAllNotificationsQuery : IRequest<List<NotificationDetailDto>>
{
}

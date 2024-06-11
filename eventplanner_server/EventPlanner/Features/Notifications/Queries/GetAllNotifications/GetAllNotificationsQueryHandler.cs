using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EventPlanner.Features;
public class GetAllNotificationsQueryHandler : IRequestHandler<GetAllNotificationsQuery, List<NotificationDetailDto>>
{
    private readonly ApplicationDbContext _context;

    public GetAllNotificationsQueryHandler(ApplicationDbContext aContext)
    {
        _context = aContext;
    }

    public async Task<List<NotificationDetailDto>> Handle(GetAllNotificationsQuery aRequest, CancellationToken aCancellationToken)
    {
        var vNotifications = await _context.Notifications.ToListAsync(aCancellationToken);

        var vNotificationDtos = new List<NotificationDetailDto>();

        foreach (var vNotificationEntity in vNotifications)
        {
            vNotificationDtos.Add(new NotificationDetailDto
            {
                Id = vNotificationEntity.Id,
                Message = vNotificationEntity.Message,
                Date = vNotificationEntity.Date
            });
        }

        return vNotificationDtos;
    }
}

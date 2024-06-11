using MediatR;

namespace EventPlanner.Features;
    public class GetNotificationByIdQueryHandler : IRequestHandler<GetNotificationByIdQuery, NotificationDetailDto>
    {
        private readonly ApplicationDbContext _context;

        public GetNotificationByIdQueryHandler(ApplicationDbContext aContext)
        {
            _context = aContext;
        }

        public async Task<NotificationDetailDto> Handle(GetNotificationByIdQuery aRequest, CancellationToken aCancellationToken)
        {
            var vNotificationEntity = await _context.Notifications.FindAsync(aRequest.Id);

            if (vNotificationEntity == null)
            {
                return null;
            }

            return new NotificationDetailDto
            {
                Id = vNotificationEntity.Id,
                Message = vNotificationEntity.Message,
                Date = vNotificationEntity.Date
            };
        }
    }

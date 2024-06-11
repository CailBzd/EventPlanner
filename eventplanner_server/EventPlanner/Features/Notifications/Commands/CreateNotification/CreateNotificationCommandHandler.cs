using MediatR;
using EventPlanner.Models;

namespace EventPlanner.Features;
    public class CreateNotificationCommandHandler : IRequestHandler<CreateNotificationCommand, Guid>
    {
        private readonly ApplicationDbContext _context;

        public CreateNotificationCommandHandler(ApplicationDbContext aContext)
        {
            _context = aContext;
        }

        public async Task<Guid> Handle(CreateNotificationCommand aRequest, CancellationToken aCancellationToken)
        {
            var vNotificationEntity = new Notification
            {
                Id = Guid.NewGuid(),
                Message = aRequest.Message,
                Date = aRequest.Date
            };

            await _context.Notifications.AddAsync(vNotificationEntity, aCancellationToken);
            await _context.SaveChangesAsync(aCancellationToken);

            return vNotificationEntity.Id;
        }
    }

using MediatR;

namespace EventPlanner.Features;
    public class UpdateNotificationCommandHandler : IRequestHandler<UpdateNotificationCommand, bool>
    {
        private readonly ApplicationDbContext _context;

        public UpdateNotificationCommandHandler(ApplicationDbContext aContext)
        {
            _context = aContext;
        }

        public async Task<bool> Handle(UpdateNotificationCommand aRequest, CancellationToken aCancellationToken)
        {
            var vNotificationEntity = await _context.Notifications.FindAsync(aRequest.Id);

            if (vNotificationEntity == null)
            {
                return false;
            }

            vNotificationEntity.Message = aRequest.Message;
            vNotificationEntity.Date = aRequest.Date;

            _context.Notifications.Update(vNotificationEntity);
            await _context.SaveChangesAsync(aCancellationToken);

            return true;
        }
    }

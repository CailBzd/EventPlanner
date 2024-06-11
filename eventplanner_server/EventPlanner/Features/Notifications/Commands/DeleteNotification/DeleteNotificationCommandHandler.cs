
using MediatR;

namespace EventPlanner.Features;
public class DeleteNotificationCommandHandler : IRequestHandler<DeleteNotificationCommand, bool>
{
    private readonly ApplicationDbContext _context;

    public DeleteNotificationCommandHandler(ApplicationDbContext aContext)
    {
        _context = aContext;
    }

    public async Task<bool> Handle(DeleteNotificationCommand aRequest, CancellationToken aCancellationToken)
    {
        var vNotificationEntity = await _context.Notifications.FindAsync(aRequest.Id);

        if (vNotificationEntity == null)
        {
            return false;
        }

        _context.Notifications.Remove(vNotificationEntity);
        await _context.SaveChangesAsync(aCancellationToken);

        return true;
    }
}

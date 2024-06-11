using MediatR;
namespace EventPlanner.Features;
public class DeleteSubscriptionCommandHandler : IRequestHandler<DeleteSubscriptionCommand, bool>
{
    private readonly ApplicationDbContext _context;

    public DeleteSubscriptionCommandHandler(ApplicationDbContext aContext)
    {
        _context = aContext;
    }

    public async Task<bool> Handle(DeleteSubscriptionCommand aRequest, CancellationToken aCancellationToken)
    {
        var vSubscriptionEntity = await _context.Subscriptions.FindAsync(aRequest.Id);

        if (vSubscriptionEntity == null)
        {
            return false;
        }

        _context.Subscriptions.Remove(vSubscriptionEntity);
        await _context.SaveChangesAsync(aCancellationToken);

        return true;
    }
}

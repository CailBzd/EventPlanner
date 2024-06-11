using MediatR;

namespace EventPlanner.Features;
public class UpdateSubscriptionCommandHandler : IRequestHandler<UpdateSubscriptionCommand, bool>
{
    private readonly ApplicationDbContext _context;

    public UpdateSubscriptionCommandHandler(ApplicationDbContext aContext)
    {
        _context = aContext;
    }

    public async Task<bool> Handle(UpdateSubscriptionCommand aRequest, CancellationToken aCancellationToken)
    {
        var vSubscriptionEntity = await _context.Subscriptions.FindAsync(aRequest.Id);

        if (vSubscriptionEntity == null)
        {
            return false;
        }

        vSubscriptionEntity.Plan = aRequest.Plan;
        vSubscriptionEntity.StartDate = aRequest.StartDate;
        vSubscriptionEntity.EndDate = aRequest.EndDate;

        _context.Subscriptions.Update(vSubscriptionEntity);
        await _context.SaveChangesAsync(aCancellationToken);

        return true;
    }
}


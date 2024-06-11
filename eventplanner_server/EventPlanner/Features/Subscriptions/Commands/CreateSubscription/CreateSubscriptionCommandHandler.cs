using MediatR;
using EventPlanner.Models;

namespace EventPlanner.Features;
public class CreateSubscriptionCommandHandler : IRequestHandler<CreateSubscriptionCommand, Guid>
{
    private readonly ApplicationDbContext _context;

    public CreateSubscriptionCommandHandler(ApplicationDbContext aContext)
    {
        _context = aContext;
    }

    public async Task<Guid> Handle(CreateSubscriptionCommand aRequest, CancellationToken aCancellationToken)
    {
        var vSubscriptionEntity = new Subscription
        {
            Id = Guid.NewGuid(),
            UserId = aRequest.UserId,
            Plan = aRequest.Plan,
            StartDate = aRequest.StartDate,
            EndDate = aRequest.EndDate
        };

        await _context.Subscriptions.AddAsync(vSubscriptionEntity, aCancellationToken);
        await _context.SaveChangesAsync(aCancellationToken);

        return vSubscriptionEntity.Id;
    }
}


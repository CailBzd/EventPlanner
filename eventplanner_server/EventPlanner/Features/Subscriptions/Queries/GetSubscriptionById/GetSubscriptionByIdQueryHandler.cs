using MediatR;

namespace EventPlanner.Features;
public class GetSubscriptionByIdQueryHandler : IRequestHandler<GetSubscriptionByIdQuery, SubscriptionDetailDto>
{
    private readonly ApplicationDbContext _context;

    public GetSubscriptionByIdQueryHandler(ApplicationDbContext aContext)
    {
        _context = aContext;
    }

    public async Task<SubscriptionDetailDto> Handle(GetSubscriptionByIdQuery aRequest, CancellationToken aCancellationToken)
    {
        var vSubscriptionEntity = await _context.Subscriptions.FindAsync(aRequest.Id);

        if (vSubscriptionEntity == null)
        {
            return null;
        }

        return new SubscriptionDetailDto
        {
            Id = vSubscriptionEntity.Id,
            UserId = vSubscriptionEntity.UserId,
            Plan = vSubscriptionEntity.Plan,
            StartDate = vSubscriptionEntity.StartDate,
            EndDate = vSubscriptionEntity.EndDate
        };
    }
}


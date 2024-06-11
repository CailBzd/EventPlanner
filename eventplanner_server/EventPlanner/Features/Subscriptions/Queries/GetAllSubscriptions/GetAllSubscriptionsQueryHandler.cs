using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EventPlanner.Features;
public class GetAllSubscriptionsQueryHandler : IRequestHandler<GetAllSubscriptionsQuery, List<SubscriptionDetailDto>>
{
    private readonly ApplicationDbContext _context;

    public GetAllSubscriptionsQueryHandler(ApplicationDbContext aContext)
    {
        _context = aContext;
    }

    public async Task<List<SubscriptionDetailDto>> Handle(GetAllSubscriptionsQuery aRequest, CancellationToken aCancellationToken)
    {
        var vSubscriptions = await _context.Subscriptions.ToListAsync(aCancellationToken);

        var vSubscriptionDtos = new List<SubscriptionDetailDto>();

        foreach (var vSubscriptionEntity in vSubscriptions)
        {
            vSubscriptionDtos.Add(new SubscriptionDetailDto
            {
                Id = vSubscriptionEntity.Id,
                UserId = vSubscriptionEntity.UserId,
                Plan = vSubscriptionEntity.Plan,
                Price = vSubscriptionEntity.Price,
                StartDate = vSubscriptionEntity.StartDate,
                EndDate = vSubscriptionEntity.EndDate
            });
        }

        return vSubscriptionDtos;
    }
}

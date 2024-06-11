using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EventPlanner.Features;
public class GetAllEventsQueryHandler : IRequestHandler<GetAllEventsQuery, List<EventDetailDto>>
{
    private readonly ApplicationDbContext _context;

    public GetAllEventsQueryHandler(ApplicationDbContext aContext)
    {
        _context = aContext;
    }

    public async Task<List<EventDetailDto>> Handle(GetAllEventsQuery aRequest, CancellationToken aCancellationToken)
    {
        var vEvents = await _context.Events.ToListAsync(aCancellationToken);

        var vEventDtos = new List<EventDetailDto>();

        foreach (var vEventEntity in vEvents)
        {
            vEventDtos.Add(new EventDetailDto
            {
                Id = vEventEntity.Id,
                Title = vEventEntity.Title,
                Description = vEventEntity.Description,
                StartDate = vEventEntity.StartDate,
                EndDate = vEventEntity.EndDate,
                Location = vEventEntity.Location
            });
        }

        return vEventDtos;
    }
}

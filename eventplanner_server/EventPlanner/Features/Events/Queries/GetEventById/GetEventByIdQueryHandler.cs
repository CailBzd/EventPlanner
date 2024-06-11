using MediatR;

namespace EventPlanner.Features;
public class GetEventByIdQueryHandler : IRequestHandler<GetEventByIdQuery, EventDetailDto>
{
    private readonly ApplicationDbContext _context;

    public GetEventByIdQueryHandler(ApplicationDbContext aContext)
    {
        _context = aContext;
    }

    public async Task<EventDetailDto> Handle(GetEventByIdQuery aRequest, CancellationToken aCancellationToken)
    {
        var vEventEntity = await _context.Events.FindAsync(aRequest.Id);

        if (vEventEntity == null)
        {
            return null;
        }

        return new EventDetailDto
        {
            Id = vEventEntity.Id,
            Title = vEventEntity.Title,
            Description = vEventEntity.Description,
            StartDate = vEventEntity.StartDate,
            EndDate = vEventEntity.EndDate,
            Location = vEventEntity.Location
        };
    }
}


using EventPlanner.Models;
using MediatR;

namespace EventPlanner.Features;
public class CreateEventCommandHandler : IRequestHandler<CreateEventCommand, Guid>
{
    private readonly ApplicationDbContext _context;

    public CreateEventCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateEventCommand request, CancellationToken cancellationToken)
    {
        var eventEntity = new Event
        {
            Id = Guid.NewGuid(),
            Title = request.Title,
            Description = request.Description,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            Location = request.Location
        };

        _context.Events.Add(eventEntity);
        await _context.SaveChangesAsync(cancellationToken);

        return eventEntity.Id;
    }
}

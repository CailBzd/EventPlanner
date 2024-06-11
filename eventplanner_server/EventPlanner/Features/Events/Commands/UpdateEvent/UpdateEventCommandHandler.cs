using MediatR;

namespace EventPlanner.Features;
public class UpdateEventCommandHandler : IRequestHandler<UpdateEventCommand, bool>
{
    private readonly ApplicationDbContext _context;

    public UpdateEventCommandHandler(ApplicationDbContext aContext)
    {
        _context = aContext;
    }

    public async Task<bool> Handle(UpdateEventCommand aRequest, CancellationToken aCancellationToken)
    {
        var vEventEntity = await _context.Events.FindAsync(aRequest.Id);

        if (vEventEntity == null)
        {
            return false;
        }

        vEventEntity.Title = aRequest.Title;
        vEventEntity.Description = aRequest.Description;
        vEventEntity.StartDate = aRequest.StartDate;
        vEventEntity.EndDate = aRequest.EndDate;
        vEventEntity.Location = aRequest.Location;

        _context.Events.Update(vEventEntity);
        await _context.SaveChangesAsync(aCancellationToken);

        return true;
    }
}



using MediatR;

namespace EventPlanner.Features;
public class DeleteEventCommandHandler : IRequestHandler<DeleteEventCommand, bool>
{
    private readonly ApplicationDbContext _context;

    public DeleteEventCommandHandler(ApplicationDbContext aContext)
    {
        _context = aContext;
    }

    public async Task<bool> Handle(DeleteEventCommand aRequest, CancellationToken aCancellationToken)
    {
        var vEventEntity = await _context.Events.FindAsync(aRequest.Id);

        if (vEventEntity == null)
        {
            return false;
        }

        _context.Events.Remove(vEventEntity);
        await _context.SaveChangesAsync(aCancellationToken);

        return true;
    }
}

using MediatR;

namespace EventPlanner.Features;
public class DeletePollCommandHandler : IRequestHandler<DeletePollCommand, bool>
{
    private readonly ApplicationDbContext _context;

    public DeletePollCommandHandler(ApplicationDbContext aContext)
    {
        _context = aContext;
    }

    public async Task<bool> Handle(DeletePollCommand aRequest, CancellationToken aCancellationToken)
    {
        var vPollEntity = await _context.Polls.FindAsync(aRequest.Id);

        if (vPollEntity == null)
        {
            return false;
        }

        _context.Polls.Remove(vPollEntity);
        await _context.SaveChangesAsync(aCancellationToken);

        return true;
    }
}

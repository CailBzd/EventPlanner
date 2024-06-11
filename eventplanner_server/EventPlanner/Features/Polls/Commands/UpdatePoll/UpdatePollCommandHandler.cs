using MediatR;

namespace EventPlanner.Features;
public class UpdatePollCommandHandler : IRequestHandler<UpdatePollCommand, bool>
{
    private readonly ApplicationDbContext _context;

    public UpdatePollCommandHandler(ApplicationDbContext aContext)
    {
        _context = aContext;
    }

    public async Task<bool> Handle(UpdatePollCommand aRequest, CancellationToken aCancellationToken)
    {
        var vPollEntity = await _context.Polls.FindAsync(aRequest.Id);

        if (vPollEntity == null)
        {
            return false;
        }

        vPollEntity.Question = aRequest.Question;

        _context.Polls.Update(vPollEntity);
        await _context.SaveChangesAsync(aCancellationToken);

        return true;
    }
}

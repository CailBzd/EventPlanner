using MediatR;

namespace EventPlanner.Features;

public class DeleteGroupCommandHandler : IRequestHandler<DeleteGroupCommand, bool>
{
    private readonly ApplicationDbContext _context;

    public DeleteGroupCommandHandler(ApplicationDbContext aContext)
    {
        _context = aContext;
    }

    public async Task<bool> Handle(DeleteGroupCommand aRequest, CancellationToken aCancellationToken)
    {
        var vGroupEntity = await _context.Groups.FindAsync(aRequest.Id);

        if (vGroupEntity == null)
        {
            return false;
        }

        _context.Groups.Remove(vGroupEntity);
        await _context.SaveChangesAsync(aCancellationToken);

        return true;
    }
}
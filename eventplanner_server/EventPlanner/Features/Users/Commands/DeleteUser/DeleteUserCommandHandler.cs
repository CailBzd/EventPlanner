using MediatR;

namespace EventPlanner.Features;
public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, bool>
{
    private readonly ApplicationDbContext _context;

    public DeleteUserCommandHandler(ApplicationDbContext aContext)
    {
        _context = aContext;
    }

    public async Task<bool> Handle(DeleteUserCommand aRequest, CancellationToken aCancellationToken)
    {
        var vUserEntity = await _context.Users.FindAsync(aRequest.Id);

        if (vUserEntity == null)
        {
            return false;
        }

        _context.Users.Remove(vUserEntity);
        await _context.SaveChangesAsync(aCancellationToken);

        return true;
    }
}

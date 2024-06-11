using MediatR;

namespace EventPlanner.Features;
public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, bool>
{
    private readonly ApplicationDbContext _context;

    public UpdateUserCommandHandler(ApplicationDbContext aContext)
    {
        _context = aContext;
    }

    public async Task<bool> Handle(UpdateUserCommand aRequest, CancellationToken aCancellationToken)
    {
        var vUserEntity = await _context.Users.FindAsync(aRequest.Id);

        if (vUserEntity == null)
        {
            return false;
        }

        vUserEntity.UserName = aRequest.UserName;
        vUserEntity.Email = aRequest.Email;
        vUserEntity.PasswordHash = aRequest.PasswordHash;

        _context.Users.Update(vUserEntity);
        await _context.SaveChangesAsync(aCancellationToken);

        return true;
    }
}


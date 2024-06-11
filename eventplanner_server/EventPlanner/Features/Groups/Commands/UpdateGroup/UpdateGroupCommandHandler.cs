using MediatR;

namespace EventPlanner.Features;
public class UpdateGroupCommandHandler : IRequestHandler<UpdateGroupCommand, bool>
{
    private readonly ApplicationDbContext _context;

    public UpdateGroupCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(UpdateGroupCommand aRequest, CancellationToken aCancellationToken)
    {
        var vGroupEntity = await _context.Groups.FindAsync(aRequest.Id);

        if (vGroupEntity == null)
        {
            return false;
        }

        vGroupEntity.Name = aRequest.Name;

        _context.Groups.Update(vGroupEntity);
        await _context.SaveChangesAsync(aCancellationToken);

        return true;
    }
}
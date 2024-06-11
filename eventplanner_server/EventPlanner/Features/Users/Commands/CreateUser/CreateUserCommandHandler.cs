using MediatR;
using EventPlanner.Models;

namespace EventPlanner.Features;
public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
{
    private readonly ApplicationDbContext _context;

    public CreateUserCommandHandler(ApplicationDbContext aContext)
    {
        _context = aContext;
    }

    public async Task<Guid> Handle(CreateUserCommand aRequest, CancellationToken aCancellationToken)
    {
        var vUserEntity = new User
        {
            Id = Guid.NewGuid(),
            UserName = aRequest.UserName,
            Email = aRequest.Email,
            PasswordHash = aRequest.PasswordHash
        };

        await _context.Users.AddAsync(vUserEntity, aCancellationToken);
        await _context.SaveChangesAsync(aCancellationToken);

        return vUserEntity.Id;
    }
}

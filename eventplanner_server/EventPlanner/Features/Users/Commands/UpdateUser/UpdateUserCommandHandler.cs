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


        vUserEntity.UserName = aRequest.UserName ?? vUserEntity.UserName;
        vUserEntity.Email = aRequest.Email ?? vUserEntity.Email;
        vUserEntity.Biography = aRequest.Biography ?? vUserEntity.Biography;
        vUserEntity.ProfilePicture = aRequest.ProfilePicture ?? vUserEntity.ProfilePicture;
        vUserEntity.City = aRequest.City ?? vUserEntity.City;
        vUserEntity.PostalCode = aRequest.PostalCode ?? vUserEntity.PostalCode;
        vUserEntity.Country = aRequest.Country ?? vUserEntity.Country;
        vUserEntity.DateOfBirth = aRequest.DateOfBirth != default ? aRequest.DateOfBirth : vUserEntity.DateOfBirth;
        vUserEntity.PhoneNumber = aRequest.PhoneNumber ?? vUserEntity.PhoneNumber;

        _context.Users.Update(vUserEntity);
        await _context.SaveChangesAsync(aCancellationToken);

        return true;
    }
}


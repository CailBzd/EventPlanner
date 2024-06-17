using MediatR;
using EventPlanner.Models;
using Microsoft.AspNetCore.Identity;

namespace EventPlanner.Features;
public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
{
    private readonly UserManager<User> _userManager;

    public CreateUserCommandHandler(UserManager<User> aUserManager)
    {
        _userManager = aUserManager;
    }

    public async Task<Guid> Handle(CreateUserCommand aRequest, CancellationToken aCancellationToken)
    {
        var vUser = new User
        {
            UserName = aRequest.UserName,
            Email = aRequest.Email,
            PasswordHash = aRequest.Password
        };

        await _userManager.CreateAsync(vUser, aRequest.Password);
        await _userManager.AddToRoleAsync(vUser, "User");

        return vUser.Id;
    }
}

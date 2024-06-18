using EventPlanner.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace EventPlanner.Features;
public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, bool>
{
    private readonly UserManager<User> _userManager;

    public ResetPasswordCommandHandler(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<bool> Handle(ResetPasswordCommand aRequest, CancellationToken aCancellationToken)
    {
        var vUser = await _userManager.FindByEmailAsync(aRequest.Email);
        if (vUser == null)
        {
            return false;
        }

        var vResult = await _userManager.ResetPasswordAsync(vUser, aRequest.Token, aRequest.NewPassword);
        return vResult.Succeeded;
    }
}

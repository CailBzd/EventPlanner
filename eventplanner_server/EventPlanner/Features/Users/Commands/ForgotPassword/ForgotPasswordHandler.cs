using EventPlanner.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace EventPlanner.Features;
public class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommand, bool>
{
    private readonly UserManager<User> _userManager;
    private readonly IEmailSender _emailSender;

    public ForgotPasswordCommandHandler(UserManager<User> aUserManager, IEmailSender aEmailSender)
    {
        _userManager = aUserManager;
        _emailSender = aEmailSender;
    }

    public async Task<bool> Handle(ForgotPasswordCommand aRequest, CancellationToken aCancellationToken)
    {
        var vUser = await _userManager.FindByEmailAsync(aRequest.Email);
        if (vUser == null)
        {
            return false;
        }

        var vToken = await _userManager.GeneratePasswordResetTokenAsync(vUser);
        var vResetLink = $"https://yourfrontend.com/reset-password?token={vToken}&email={aRequest.Email}";

        await _emailSender.SendEmailAsync(aRequest.Email, "Reset Password", $"<a href='{vResetLink}'>Reset Password</a>");

        return true;
    }
}

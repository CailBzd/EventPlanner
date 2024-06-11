using EventPlanner.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace EventPlanner.Features;

public class LogoutCommandHandler : IRequestHandler<LogoutCommand>
{
    private readonly SignInManager<User> _signInManager;

    public LogoutCommandHandler(SignInManager<User> aSignInManager)
    {
        _signInManager = aSignInManager;
    }

    public async Task<Unit> Handle(LogoutCommand aRequest, CancellationToken aCancellationToken)
    {
        await _signInManager.SignOutAsync();
        return Unit.Value;
    }
}

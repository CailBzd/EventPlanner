using EventPlanner.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EventPlanner.Features;

public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResult>
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IConfiguration _configuration;
    private readonly ITokenService _tokenService;

    public LoginCommandHandler(UserManager<User> aUserManager, SignInManager<User> aSignInManager, IConfiguration aConfiguration, ITokenService aTokenService)
    {
        _userManager = aUserManager;
        _signInManager = aSignInManager;
        _configuration = aConfiguration;
        _tokenService = aTokenService;
    }

    public async Task<LoginResult> Handle(LoginCommand aRequest, CancellationToken aCancellationToken)
    {
        var vUser = await _userManager.FindByNameAsync(aRequest.Username);
        if (vUser == null)
        {
            // Si l'utilisateur mais son mail au lieu de son nom d'utilisateur
            vUser = await _userManager.FindByEmailAsync(aRequest.Username);
            if (vUser == null)
            {
                throw new UnauthorizedAccessException("Invalid username or password");
            }
        }

        var vResult = await _signInManager.CheckPasswordSignInAsync(vUser, aRequest.Password, false);
        if (!vResult.Succeeded)
        {
            throw new UnauthorizedAccessException("Invalid username or password");
        }

        var vToken = _tokenService.GenerateJwtToken(vUser);
        return new LoginResult
        {
            Token = vToken,
            UserId = vUser.Id.ToString()
        };
    }


}

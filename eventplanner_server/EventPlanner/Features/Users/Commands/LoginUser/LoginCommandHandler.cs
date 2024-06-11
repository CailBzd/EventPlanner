using EventPlanner.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EventPlanner.Features;

public class LoginCommandHandler : IRequestHandler<LoginCommand, string>
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IConfiguration _configuration;

    public LoginCommandHandler(UserManager<User> aUserManager, SignInManager<User> aSignInManager, IConfiguration aConfiguration)
    {
        _userManager = aUserManager;
        _signInManager = aSignInManager;
        _configuration = aConfiguration;
    }

    public async Task<string> Handle(LoginCommand aRequest, CancellationToken aCancellationToken)
    {
        var vUser = await _userManager.FindByNameAsync(aRequest.Username);
        if (vUser == null)
        {
            throw new UnauthorizedAccessException("Invalid username or password");
        }

        var vResult = await _signInManager.CheckPasswordSignInAsync(vUser, aRequest.Password, false);
        if (!vResult.Succeeded)
        {
            throw new UnauthorizedAccessException("Invalid username or password");
        }

        return GenerateJwtToken(vUser);
    }

    private string GenerateJwtToken(User aUser)
    {
        var vClaims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, aUser.UserName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var vKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var vCreds = new SigningCredentials(vKey, SecurityAlgorithms.HmacSha256);

        var vToken = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: vClaims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: vCreds);

        return new JwtSecurityTokenHandler().WriteToken(vToken);
    }
}

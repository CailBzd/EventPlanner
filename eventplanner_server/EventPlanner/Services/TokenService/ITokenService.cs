using EventPlanner.Models;
public interface ITokenService
{
    string GenerateJwtToken(User user);
}

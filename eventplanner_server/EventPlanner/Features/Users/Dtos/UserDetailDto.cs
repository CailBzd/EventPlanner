namespace EventPlanner.Features;
public class UserDetailDto
{
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
}
public class LoginResult
{
    public string Token { get; set; }
    public string UserId { get; set; }
    public byte[] ProfilePicture { get; set;}
}
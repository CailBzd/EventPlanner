using Microsoft.AspNetCore.Identity;

namespace EventPlanner.Models;

public class User : IdentityUser<Guid>
{
    public string? Biography { get; set; }
    public byte[]?  ProfilePicture { get; set; }
    public string? City { get; set; }
    public string? PostalCode { get; set; }
    public string? Country { get; set; }
    public DateTime? DateOfBirth { get; set; }
}

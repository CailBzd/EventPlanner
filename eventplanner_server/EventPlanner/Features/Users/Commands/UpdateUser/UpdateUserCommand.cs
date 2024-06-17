using MediatR;

namespace EventPlanner.Features;
public class UpdateUserCommand : IRequest<bool>
{
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public string? Email { get; set; }
    public string? Biography { get; set; }
    public byte[]? ProfilePicture { get; set; }
    public string? City { get; set; }
    public string? PostalCode { get; set; }
    public string? Country { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string? PhoneNumber { get; set; }
}

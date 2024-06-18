using MediatR;

namespace EventPlanner.Features;
public class ForgotPasswordCommand : IRequest<bool>
{
    public string Email { get; set; }
}
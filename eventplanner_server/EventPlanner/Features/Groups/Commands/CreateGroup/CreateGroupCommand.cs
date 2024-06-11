using MediatR;

namespace EventPlanner.Features;
public class CreateGroupCommand : IRequest<Guid>
{
    public string Name { get; set; }
}
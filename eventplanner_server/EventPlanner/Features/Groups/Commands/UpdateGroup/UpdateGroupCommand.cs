using MediatR;

namespace EventPlanner.Features;
public class UpdateGroupCommand : IRequest<bool>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}

using EventPlanner.Models;
using MediatR;

namespace EventPlanner.Features;
public class GetUserByIdQuery : IRequest<User>
{
    public Guid Id { get; set; }
}


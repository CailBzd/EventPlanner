using MediatR;

namespace EventPlanner.Features;
public class GetUserByIdQuery : IRequest<UserDetailDto>
{
    public Guid Id { get; set; }
}


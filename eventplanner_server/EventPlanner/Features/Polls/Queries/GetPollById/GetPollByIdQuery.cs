using MediatR;

namespace EventPlanner.Features;
public class GetPollByIdQuery : IRequest<PollDetailDto>
{
    public Guid Id { get; set; }
}


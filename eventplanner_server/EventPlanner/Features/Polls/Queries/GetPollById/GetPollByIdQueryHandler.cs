using MediatR;

namespace EventPlanner.Features;
public class GetPollByIdQueryHandler : IRequestHandler<GetPollByIdQuery, PollDetailDto>
{
    private readonly ApplicationDbContext _context;

    public GetPollByIdQueryHandler(ApplicationDbContext aContext)
    {
        _context = aContext;
    }

    public async Task<PollDetailDto> Handle(GetPollByIdQuery aRequest, CancellationToken aCancellationToken)
    {
        var vPollEntity = await _context.Polls.FindAsync(aRequest.Id);

        if (vPollEntity == null)
        {
            return null;
        }

        return new PollDetailDto
        {
            Id = vPollEntity.Id,
            Question = vPollEntity.Question
        };
    }
}


using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EventPlanner.Features;
public class GetAllPollsQueryHandler : IRequestHandler<GetAllPollsQuery, List<PollDetailDto>>
{
    private readonly ApplicationDbContext _context;

    public GetAllPollsQueryHandler(ApplicationDbContext aContext)
    {
        _context = aContext;
    }

    public async Task<List<PollDetailDto>> Handle(GetAllPollsQuery aRequest, CancellationToken aCancellationToken)
    {
        var vPolls = await _context.Polls.ToListAsync(aCancellationToken);

        var vPollDtos = new List<PollDetailDto>();

        foreach (var vPollEntity in vPolls)
        {
            vPollDtos.Add(new PollDetailDto
            {
                Id = vPollEntity.Id,
                Question = vPollEntity.Question
            });
        }

        return vPollDtos;
    }
}

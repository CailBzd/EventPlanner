using MediatR;
using EventPlanner.Models;

namespace EventPlanner.Features;
public class CreatePollCommandHandler : IRequestHandler<CreatePollCommand, Guid>
{
    private readonly ApplicationDbContext _context;

    public CreatePollCommandHandler(ApplicationDbContext aContext)
    {
        _context = aContext;
    }

    public async Task<Guid> Handle(CreatePollCommand aRequest, CancellationToken aCancellationToken)
    {
        var vPollEntity = new Poll
        {
            Id = Guid.NewGuid(),
            Question = aRequest.Question
        };

        await _context.Polls.AddAsync(vPollEntity, aCancellationToken);
        await _context.SaveChangesAsync(aCancellationToken);

        return vPollEntity.Id;
    }
}

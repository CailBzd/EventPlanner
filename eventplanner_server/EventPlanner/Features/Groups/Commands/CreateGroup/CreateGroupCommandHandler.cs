using MediatR;
using EventPlanner.Models;

namespace EventPlanner.Features;
    public class CreateGroupCommandHandler : IRequestHandler<CreateGroupCommand, Guid>
    {
        private readonly ApplicationDbContext _context;

        public CreateGroupCommandHandler(ApplicationDbContext aContext)
        {
            _context = aContext;
        }

        public async Task<Guid> Handle(CreateGroupCommand aRequest, CancellationToken aCancellationToken)
        {
            var vGroupEntity = new Group
            {
                Id = Guid.NewGuid(),
                Name = aRequest.Name
            };

            await _context.Groups.AddAsync(vGroupEntity, aCancellationToken);
            await _context.SaveChangesAsync(aCancellationToken);

            return vGroupEntity.Id;
        }
    }

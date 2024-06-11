using MediatR;

namespace EventPlanner.Features;
    public class GetGroupByIdQueryHandler : IRequestHandler<GetGroupByIdQuery, GroupDetailDto>
    {
        private readonly ApplicationDbContext _context;

        public GetGroupByIdQueryHandler(ApplicationDbContext aContext)
        {
            _context = aContext;
        }

        public async Task<GroupDetailDto> Handle(GetGroupByIdQuery aRequest, CancellationToken aCancellationToken)
        {
            var vGroupEntity = await _context.Groups.FindAsync(aRequest.Id);

            if (vGroupEntity == null)
            {
                return null;
            }

            return new GroupDetailDto
            {
                Id = vGroupEntity.Id,
                Name = vGroupEntity.Name
            };
        }
    }

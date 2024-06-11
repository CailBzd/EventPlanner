using MediatR;

namespace EventPlanner.Features;
    public class GetGroupByIdQuery : IRequest<GroupDetailDto>
    {
        public Guid Id { get; set; }
    }

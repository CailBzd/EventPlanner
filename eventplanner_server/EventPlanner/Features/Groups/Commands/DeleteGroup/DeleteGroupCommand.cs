using MediatR;

namespace EventPlanner.Features;
    public class DeleteGroupCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }
using MediatR;

namespace EventPlanner.Features;
    public class DeleteFileCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }


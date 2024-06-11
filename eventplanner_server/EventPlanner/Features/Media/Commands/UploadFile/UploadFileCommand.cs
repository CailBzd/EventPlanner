using MediatR;

namespace EventPlanner.Features;
    public class UploadFileCommand : IRequest<Guid>
    {
        public IFormFile File { get; set; }
    }


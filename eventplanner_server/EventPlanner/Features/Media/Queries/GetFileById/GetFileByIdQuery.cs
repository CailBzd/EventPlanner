using MediatR;

namespace EventPlanner.Features;
public class GetFileByIdQuery : IRequest<FileDetailDto>
{
    public Guid Id { get; set; }
}

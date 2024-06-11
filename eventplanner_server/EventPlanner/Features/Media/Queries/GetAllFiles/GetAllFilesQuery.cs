using MediatR;

namespace EventPlanner.Features;
public class GetAllFilesQuery : IRequest<List<FileDetailDto>>
{
}
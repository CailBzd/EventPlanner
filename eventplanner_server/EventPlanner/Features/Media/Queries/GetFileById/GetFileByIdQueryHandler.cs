using MediatR;

namespace EventPlanner.Features;
public class GetFileByIdQueryHandler : IRequestHandler<GetFileByIdQuery, FileDetailDto>
{
    private readonly ApplicationDbContext _context;

    public GetFileByIdQueryHandler(ApplicationDbContext aContext)
    {
        _context = aContext;
    }

    public async Task<FileDetailDto> Handle(GetFileByIdQuery aRequest, CancellationToken aCancellationToken)
    {
        var vFileEntity = await _context.MediaFiles.FindAsync(aRequest.Id);

        if (vFileEntity == null)
        {
            return null;
        }

        return new FileDetailDto
        {
            Id = vFileEntity.Id,
            FileName = vFileEntity.FileName,
            FilePath = vFileEntity.FilePath,
            UploadDate = vFileEntity.UploadDate
        };
    }
}

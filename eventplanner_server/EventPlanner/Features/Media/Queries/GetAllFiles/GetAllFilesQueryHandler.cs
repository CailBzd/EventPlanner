using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EventPlanner.Features;
public class GetAllFilesQueryHandler : IRequestHandler<GetAllFilesQuery, List<FileDetailDto>>
{
    private readonly ApplicationDbContext _context;

    public GetAllFilesQueryHandler(ApplicationDbContext aContext)
    {
        _context = aContext;
    }

    public async Task<List<FileDetailDto>> Handle(GetAllFilesQuery aRequest, CancellationToken aCancellationToken)
    {
        var vFiles = await _context.MediaFiles.ToListAsync(aCancellationToken);

        var vFileDtos = new List<FileDetailDto>();

        foreach (var vFileEntity in vFiles)
        {
            vFileDtos.Add(new FileDetailDto
            {
                Id = vFileEntity.Id,
                FileName = vFileEntity.FileName,
                FilePath = vFileEntity.FilePath,
                UploadDate = vFileEntity.UploadDate
            });
        }

        return vFileDtos;
    }
}


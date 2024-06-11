using EventPlanner.Models;
using MediatR;

namespace EventPlanner.Features;
public class UploadFileCommandHandler : IRequestHandler<UploadFileCommand, Guid>
{
    private readonly ApplicationDbContext _context;
    private readonly IWebHostEnvironment _environment;

    public UploadFileCommandHandler(ApplicationDbContext aContext, IWebHostEnvironment aEnvironment)
    {
        _context = aContext;
        _environment = aEnvironment;
    }

    public async Task<Guid> Handle(UploadFileCommand aRequest, CancellationToken aCancellationToken)
    {
        if (aRequest.File == null || aRequest.File.Length == 0)
        {
            throw new ArgumentException("Invalid file.");
        }

        var vFileName = Guid.NewGuid().ToString() + Path.GetExtension(aRequest.File.FileName);
        var vFilePath = Path.Combine(_environment.WebRootPath, "uploads", vFileName);

        using (var vStream = new FileStream(vFilePath, FileMode.Create))
        {
            await aRequest.File.CopyToAsync(vStream, aCancellationToken);
        }

        var vFile = new MediaFile
        {
            Id = Guid.NewGuid(),
            FileName = aRequest.File.FileName,
            FilePath = vFilePath,
            UploadDate = DateTime.UtcNow
        };

        _context.MediaFiles.Add(vFile);
        await _context.SaveChangesAsync(aCancellationToken);

        return vFile.Id;
    }
}

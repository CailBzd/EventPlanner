using MediatR;

namespace EventPlanner.Features;
    public class DeleteFileCommandHandler : IRequestHandler<DeleteFileCommand, bool>
    {
        private readonly ApplicationDbContext _context;

        public DeleteFileCommandHandler(ApplicationDbContext aContext)
        {
            _context = aContext;
        }

        public async Task<bool> Handle(DeleteFileCommand aRequest, CancellationToken aCancellationToken)
        {
            var vFileEntity = await _context.MediaFiles.FindAsync(aRequest.Id);

            if (vFileEntity == null)
            {
                return false;
            }

            _context.MediaFiles.Remove(vFileEntity);
            await _context.SaveChangesAsync(aCancellationToken);

            if (File.Exists(vFileEntity.FilePath))
            {
            File.Delete(vFileEntity.FilePath);
            }

            return true;
        }
    }

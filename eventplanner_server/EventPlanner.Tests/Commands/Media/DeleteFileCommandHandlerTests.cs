using Xunit;
using EventPlanner.Models;
using Microsoft.EntityFrameworkCore;
using EventPlanner.Features;

namespace EventPlanner.Tests;

public class DeleteFileCommandHandlerTests
{
    private readonly ApplicationDbContext _context;
    private readonly DeleteFileCommandHandler _handler;

    public DeleteFileCommandHandlerTests()
    {
        var vOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;
        _context = new ApplicationDbContext(vOptions);
        _handler = new DeleteFileCommandHandler(_context);
    }

    [Fact]
    public async Task Handle_GivenValidRequest_ShouldDeleteFile()
    {
        // Arrange
        var vFileId = Guid.NewGuid();
        var vFilePath = Path.Combine(Directory.GetCurrentDirectory(), "uploads", $"{vFileId}.txt");
        Directory.CreateDirectory(Path.GetDirectoryName(vFilePath));
        await System.IO.File.WriteAllTextAsync(vFilePath, "Test content");

        var vFileEntity = new MediaFile
        {
            Id = vFileId,
            FileName = $"{vFileId}.txt",
            FilePath = vFilePath,
            UploadDate = DateTime.UtcNow
        };

        await _context.MediaFiles.AddAsync(vFileEntity);
        await _context.SaveChangesAsync();

        var aCommand = new DeleteFileCommand { Id = vFileId };

        // Act
        var vResult = await _handler.Handle(aCommand, CancellationToken.None);

        // Assert
        Assert.True(vResult);
        Assert.Null(await _context.MediaFiles.FindAsync(vFileId));
        Assert.False(System.IO.File.Exists(vFilePath));
    }

    [Fact]
    public async Task Handle_GivenInvalidRequest_ShouldReturnFalse()
    {
        // Arrange
        var aCommand = new DeleteFileCommand { Id = Guid.NewGuid() };

        // Act
        var vResult = await _handler.Handle(aCommand, CancellationToken.None);

        // Assert
        Assert.False(vResult);
    }
}

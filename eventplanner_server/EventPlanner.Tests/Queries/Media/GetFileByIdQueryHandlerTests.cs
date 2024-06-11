using Xunit;
using EventPlanner.Models;
using Microsoft.EntityFrameworkCore;
using EventPlanner.Features;

namespace EventPlanner.Tests;

public class GetFileByIdQueryHandlerTests
{
    private readonly ApplicationDbContext _context;
    private readonly GetFileByIdQueryHandler _handler;

    public GetFileByIdQueryHandlerTests()
    {
        var vOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;
        _context = new ApplicationDbContext(vOptions);
        _handler = new GetFileByIdQueryHandler(_context);
    }

    [Fact]
    public async Task Handle_GivenValidId_ShouldReturnFileDetail()
    {
        // Arrange
        var vFileId = Guid.NewGuid();
        var vFileEntity = new MediaFile
        {
            Id = vFileId,
            FileName = "test.txt",
            FilePath = Path.Combine(Directory.GetCurrentDirectory(), "uploads", "test.txt"),
            UploadDate = DateTime.UtcNow
        };

        await _context.MediaFiles.AddAsync(vFileEntity);
        await _context.SaveChangesAsync();

        var aQuery = new GetFileByIdQuery { Id = vFileId };

        // Act
        var vResult = await _handler.Handle(aQuery, CancellationToken.None);

        // Assert
        Assert.NotNull(vResult);
        Assert.Equal(vFileId, vResult.Id);
        Assert.Equal("test.txt", vResult.FileName);
    }

    [Fact]
    public async Task Handle_GivenInvalidId_ShouldReturnNull()
    {
        // Arrange
        var aQuery = new GetFileByIdQuery { Id = Guid.NewGuid() };

        // Act
        var vResult = await _handler.Handle(aQuery, CancellationToken.None);

        // Assert
        Assert.Null(vResult);
    }
}

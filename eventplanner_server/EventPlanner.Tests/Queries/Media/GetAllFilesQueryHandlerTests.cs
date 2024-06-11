using Xunit;
using EventPlanner.Models;
using Microsoft.EntityFrameworkCore;
using EventPlanner.Features;

namespace EventPlanner.Tests;

public class GetAllFilesQueryHandlerTests
{
    private readonly ApplicationDbContext _context;
    private readonly GetAllFilesQueryHandler _handler;

    public GetAllFilesQueryHandlerTests()
    {
        var vOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;
        _context = new ApplicationDbContext(vOptions);
        _handler = new GetAllFilesQueryHandler(_context);
    }

    private async Task SeedDatabase()
    {
        var vFiles = new List<MediaFile>
        {
            new MediaFile { Id = Guid.NewGuid(), FileName = "test1.txt", FilePath = "path/to/test1.txt", UploadDate = DateTime.UtcNow },
            new MediaFile { Id = Guid.NewGuid(), FileName = "test2.txt", FilePath = "path/to/test2.txt", UploadDate = DateTime.UtcNow }
        };

        await _context.MediaFiles.AddRangeAsync(vFiles);
        await _context.SaveChangesAsync();

    }

    [Fact]
    public async Task Handle_ShouldReturnListOfFileDetails()
    {
        // Arrange
        await SeedDatabase();

        // Act
        var vResult = await _handler.Handle(new GetAllFilesQuery(), CancellationToken.None);

        // Assert
        Assert.NotNull(vResult);
        Assert.Equal(2, vResult.Count);
        Assert.Contains(vResult, e => e.FileName == "test1.txt");
        Assert.Contains(vResult, e => e.FileName == "test2.txt");
        Assert.Contains(vResult, e => e.FilePath == "path/to/test1.txt");
        Assert.Contains(vResult, e => e.FilePath == "path/to/test2.txt");
    }
}

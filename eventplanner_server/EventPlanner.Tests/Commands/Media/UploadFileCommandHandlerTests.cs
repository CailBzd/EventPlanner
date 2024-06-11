using Xunit;
using Moq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using EventPlanner.Features;

namespace EventPlanner.Tests;

public class UploadFileCommandHandlerTests
{
    private readonly ApplicationDbContext _context;
    private readonly UploadFileCommandHandler _handler;
    private readonly Mock<IWebHostEnvironment> _mockEnvironment;

    public UploadFileCommandHandlerTests()
    {
        var vOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;
        _context = new ApplicationDbContext(vOptions);
        _mockEnvironment = new Mock<IWebHostEnvironment>();
        _mockEnvironment.Setup(m => m.WebRootPath).Returns(Directory.GetCurrentDirectory());
        _handler = new UploadFileCommandHandler(_context, _mockEnvironment.Object);
    }

    [Fact]
    public async Task Handle_GivenValidRequest_ShouldUploadFile()
    {
        // Arrange
        var vFileMock = new Mock<IFormFile>();
        var vContent = "Hello World from a Fake File";
        var vFileName = "test.txt";
        var vStream = new MemoryStream();
        var vWriter = new StreamWriter(vStream);
        vWriter.Write(vContent);
        vWriter.Flush();
        vStream.Position = 0;

        vFileMock.Setup(f => f.OpenReadStream()).Returns(vStream);
        vFileMock.Setup(f => f.FileName).Returns(vFileName);
        vFileMock.Setup(f => f.Length).Returns(vStream.Length);

        var aCommand = new UploadFileCommand { File = vFileMock.Object };

        // Act
        var vResult = await _handler.Handle(aCommand, CancellationToken.None);

        // Assert
        Assert.NotEqual(Guid.Empty, vResult);
        var vSavedFile = await _context.MediaFiles.FindAsync(vResult);
        Assert.NotNull(vSavedFile);
        Assert.Equal(vFileName, vSavedFile.FileName);


    }

    [Fact]
    public async Task Handle_GivenInvalidFile_ShouldThrowArgumentException()
    {
        // Arrange
        var aCommand = new UploadFileCommand { File = null };

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => _handler.Handle(aCommand, CancellationToken.None));
    }
}

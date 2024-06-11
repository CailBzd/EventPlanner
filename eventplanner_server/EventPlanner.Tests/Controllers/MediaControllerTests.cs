using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using EventPlanner.Controllers;
using MediatR;
using EventPlanner.Features;
using Microsoft.AspNetCore.Http;

namespace EventPlanner.Tests;

public class MediaControllerTests
{
    private readonly Mock<IMediator> _mockMediator;
    private readonly MediaController _controller;

    public MediaControllerTests()
    {
        _mockMediator = new Mock<IMediator>();
        _controller = new MediaController(_mockMediator.Object);
    }

    [Fact]
    public async Task GetFileById_ReturnsOkResult_WhenFileExists()
    {
        // Arrange
        var vFileId = Guid.NewGuid();
        var vFileDetailDto = new FileDetailDto
        {
            Id = vFileId,
            FileName = "testfile.txt",
            FilePath = "uploads/testfile.txt",
            UploadDate = DateTime.UtcNow
        };

        _mockMediator.Setup(m => m.Send(It.IsAny<GetFileByIdQuery>(), It.IsAny<CancellationToken>()))
                     .ReturnsAsync(vFileDetailDto);

        // Act
        var vResult = await _controller.GetFileById(vFileId);

        // Assert
        var vOkResult = Assert.IsType<OkObjectResult>(vResult);
        var vReturnValue = Assert.IsType<FileDetailDto>(vOkResult.Value);
        Assert.Equal(vFileId, vReturnValue.Id);
    }

    [Fact]
    public async Task GetFileById_ReturnsNotFoundResult_WhenFileDoesNotExist()
    {
        // Arrange
        _mockMediator.Setup(m => m.Send(It.IsAny<GetFileByIdQuery>(), It.IsAny<CancellationToken>()))
                     .ReturnsAsync((FileDetailDto)null);

        // Act
        var vResult = await _controller.GetFileById(Guid.NewGuid());

        // Assert
        Assert.IsType<NotFoundResult>(vResult);
    }

    [Fact]
    public async Task GetAllFiles_ReturnsOkResult_WithListOfFiles()
    {
        // Arrange
        var vFiles = new List<FileDetailDto>
            {
                new FileDetailDto { Id = Guid.NewGuid(), FileName = "testfile1.txt", FilePath = "uploads/testfile1.txt", UploadDate = DateTime.UtcNow },
                new FileDetailDto { Id = Guid.NewGuid(), FileName = "testfile2.txt", FilePath = "uploads/testfile2.txt", UploadDate = DateTime.UtcNow }
            };

        _mockMediator.Setup(m => m.Send(It.IsAny<GetAllFilesQuery>(), It.IsAny<CancellationToken>()))
                     .ReturnsAsync(vFiles);

        // Act
        var vResult = await _controller.GetAllFiles();

        // Assert
        var vOkResult = Assert.IsType<OkObjectResult>(vResult);
        var vReturnValue = Assert.IsType<List<FileDetailDto>>(vOkResult.Value);
        Assert.Equal(vFiles.Count, vReturnValue.Count);
    }

    [Fact]
    public async Task UploadFile_ReturnsOkResult_WithFileId()
    {
        // Arrange
        var vFileId = Guid.NewGuid();
        var vFileMock = new Mock<IFormFile>();
        var vContent = "Hello World from a Fake File";
        var vFileName = "testfile.txt";
        var vStream = new MemoryStream();
        var vWriter = new StreamWriter(vStream);
        vWriter.Write(vContent);
        vWriter.Flush();
        vStream.Position = 0;
        vFileMock.Setup(_ => _.OpenReadStream()).Returns(vStream);
        vFileMock.Setup(_ => _.FileName).Returns(vFileName);
        vFileMock.Setup(_ => _.Length).Returns(vStream.Length);

        _mockMediator.Setup(m => m.Send(It.IsAny<UploadFileCommand>(), It.IsAny<CancellationToken>()))
                     .ReturnsAsync(vFileId);

        // Act
        var vResult = await _controller.UploadFile(vFileMock.Object);

        // Assert
        var vOkResult = Assert.IsType<OkObjectResult>(vResult);
        var vReturnValue = Assert.IsType<Guid>(vOkResult.Value);
        Assert.Equal(vFileId, vReturnValue);
    }

    [Fact]
    public async Task DeleteFile_ReturnsOkResult_WhenFileIsDeleted()
    {
        // Arrange
        var vFileId = Guid.NewGuid();
        _mockMediator.Setup(m => m.Send(It.IsAny<DeleteFileCommand>(), It.IsAny<CancellationToken>()))
                     .ReturnsAsync(true);

        // Act
        var vResult = await _controller.DeleteFile(vFileId);

        // Assert
        var vOkResult = Assert.IsType<OkResult>(vResult);
    }

    [Fact]
    public async Task DeleteFile_ReturnsNotFoundResult_WhenFileDoesNotExist()
    {
        // Arrange
        _mockMediator.Setup(m => m.Send(It.IsAny<DeleteFileCommand>(), It.IsAny<CancellationToken>()))
                     .ReturnsAsync(false);

        // Act
        var vResult = await _controller.DeleteFile(Guid.NewGuid());

        // Assert
        Assert.IsType<NotFoundResult>(vResult);
    }

    [Fact]
    public async Task DownloadFile_ReturnsFileResult_WhenFileExists()
    {
        // Arrange
        var vFileId = Guid.NewGuid();
        var vFileDetailDto = new FileDetailDto
        {
            Id = vFileId,
            FileName = "testfile.txt",
            FilePath = Path.GetTempFileName(),
            UploadDate = DateTime.UtcNow
        };

        await System.IO.File.WriteAllTextAsync(vFileDetailDto.FilePath, "Hello World");

        _mockMediator.Setup(m => m.Send(It.IsAny<GetFileByIdQuery>(), It.IsAny<CancellationToken>()))
                     .ReturnsAsync(vFileDetailDto);

        // Act
        var vResult = await _controller.DownloadFile(vFileId);

        // Assert
        var vFileResult = Assert.IsType<FileContentResult>(vResult);
        Assert.Equal("application/octet-stream", vFileResult.ContentType);
        Assert.Equal("testfile.txt", vFileResult.FileDownloadName);
    }

    [Fact]
    public async Task DownloadFile_ReturnsNotFoundResult_WhenFileDoesNotExist()
    {
        // Arrange
        var vFileId = Guid.NewGuid();
        var vFileDetailDto = new FileDetailDto
        {
            Id = vFileId,
            FileName = "testfile.txt",
            FilePath = "nonexistentfile.txt",
            UploadDate = DateTime.UtcNow
        };

        _mockMediator.Setup(m => m.Send(It.IsAny<GetFileByIdQuery>(), It.IsAny<CancellationToken>()))
                     .ReturnsAsync(vFileDetailDto);

        // Act
        var vResult = await _controller.DownloadFile(vFileId);

        // Assert
        Assert.IsType<NotFoundResult>(vResult);
    }
}

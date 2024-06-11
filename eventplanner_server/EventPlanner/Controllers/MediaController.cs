using EventPlanner.Features;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EventPlanner.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MediaController : ControllerBase
{
    private readonly IMediator _mediator;

    public MediaController(IMediator aMediator)
    {
        _mediator = aMediator;
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> GetFileById(Guid aId)
    {
        var vQuery = new GetFileByIdQuery { Id = aId };
        var vResult = await _mediator.Send(vQuery);

        if (vResult == null)
        {
            return NotFound();
        }

        return Ok(vResult);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllFiles()
    {
        var vQuery = new GetAllFilesQuery();
        var vResult = await _mediator.Send(vQuery);

        return Ok(vResult);
    }

    [HttpPost("upload")]
    public async Task<IActionResult> UploadFile(IFormFile aFile)
    {
        var aCommand = new UploadFileCommand { File = aFile };
        var vResult = await _mediator.Send(aCommand);
        return Ok(vResult);
    }

    [HttpGet("download/{id}")]
    public async Task<IActionResult> DownloadFile(Guid aId)
    {
        var vQuery = new GetFileByIdQuery { Id = aId };
        var vResult = await _mediator.Send(vQuery);

        if (vResult == null || !System.IO.File.Exists(vResult.FilePath))
        {
            return NotFound();
        }

        var vBytes = await System.IO.File.ReadAllBytesAsync(vResult.FilePath);
        return File(vBytes, "application/octet-stream", vResult.FileName);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteFile(Guid aId)
    {
        var vCommand = new DeleteFileCommand { Id = aId };
        var vResult = await _mediator.Send(vCommand);

        if (!vResult)
        {
            return NotFound();
        }

        return Ok();
    }

    
}


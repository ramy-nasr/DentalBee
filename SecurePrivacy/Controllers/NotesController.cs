using Application.Commands;
using Application.Queries;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class NotesController : ControllerBase
{
    private readonly IMediator _mediator;

    public NotesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Notes()
    {
        try
        {
            var query = new GeNotesQuery() { UserId = User.FindFirst("userId")?.Value };
            var notes = await _mediator.Send(query);

            return Ok(notes);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromForm] string title,
        [FromForm] string description,
        [FromForm] IFormFile? recordFile)
    {
        try
        {
            var command = new CreateNoteCommand()
            {
                Description = description,
                RecordFile = recordFile,
                Title = title,
                UserId = User.FindFirst("userId")?.Value
            };

            var note = await _mediator.Send(command);
            return Ok(note);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, [FromForm] string title,
        [FromForm] string description,
        [FromForm] IFormFile? recordFile)
    {
        try
        {
            var command = new UpdateNoteCommand()
            {
                Description = description,
                RecordFile = recordFile,
                Title = title,
                Id = id
            };

            var note = await _mediator.Send(command);
            return Ok(note);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        try
        {
            var command = new DeleteNoteCommand() { Id = id };
            await _mediator.Send(command);

            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{fileName}")]
    public async Task<FileStreamResult> DownloadAudio(string fileName)
    {
        var cts = new CancellationTokenSource(TimeSpan.FromSeconds(30));
        var query = new DownloadNoteAudioQuery() { FileName = fileName };

        var result = await _mediator.Send(query, cts.Token);

        return result;
    }
}

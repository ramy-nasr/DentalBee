using Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Commands;

public record UpdateNoteCommand : IRequest<NoteDto>
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public IFormFile? RecordFile { get; set; }
}

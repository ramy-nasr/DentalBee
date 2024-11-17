using Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Application.Commands;

public record CreateNoteCommand : IRequest<NoteDto>
{
    [JsonIgnore]
    public string? UserId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public IFormFile? RecordFile { get; set; }
}

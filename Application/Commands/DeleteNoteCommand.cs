using MediatR;

namespace Application.Commands;

public record DeleteNoteCommand : IRequest
{
    public string Id { get; set; }
}

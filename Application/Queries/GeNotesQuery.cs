using MediatR;
using System.Text.Json.Serialization;

namespace Application.Queries;

public class GeNotesQuery : IRequest<List<NoteDto>>
{
    [JsonIgnore]
    public string UserId { get; set; }
}

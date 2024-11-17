namespace Application.Queries;
public record NoteDto
{
    public string Id { get;  set; }
    public string Title { get;  set; }
    public string Description { get;  set; }
    public string RecordName { get; set; }
}
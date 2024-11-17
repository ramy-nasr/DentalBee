namespace Domain.Entities;
public class Note
{
    public string Id { get; private set; }
    public string UserId { get; private set; }
    public string Title { get; private set; }
    public string Description { get; private set; }
    public string? RecordName { get; private set; }

    public User User { get; set; }

    public Note(string userId, string title, string description, string? recordName)
    {
        Id = Guid.NewGuid().ToString();
        UserId = userId;
        Title = title;
        Description = description;
        RecordName = recordName;

    }

    public void UpdateNote(string title, string description, string? recordName)
    {
        Title = title;
        Description = description;
        RecordName = recordName;
    }
}
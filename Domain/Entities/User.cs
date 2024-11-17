namespace Domain.Entities;
public class User
{
    public string Id { get; private set; }
    public string Email { get; private set; }
    public string Password { get; private set; }

    public ICollection<Note> Notes { get; set; }

    public User(string email, string password)
    {
        Id = Guid.NewGuid().ToString();
        Email = email;
        Password = password;
    }
}
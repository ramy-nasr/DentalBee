namespace Application.Dto;

public record UserDto
{
    public string Name { get; set; }
    public string Email { get; set; }
    public DateTime CreatedAt { get; set; }
}

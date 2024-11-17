using MediatR;

namespace Application.Commands;

public record RegisterUserCommand : IRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
}

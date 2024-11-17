using MediatR;

namespace Application.Commands;

public record LoginUserCommand : IRequest<string>
{
    public string Email { get; set; }
    public string Password { get; set; }
}

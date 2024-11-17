using Domain.Repositories;
using MediatR;
using Domain.Entities;
using FluentValidation;
using Infrastructure.Interfaces;


namespace Application.Commands;

public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
{
    public LoginUserCommandValidator()
    {
        RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required.");
        RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required.").EmailAddress().WithMessage("A valid email is required.");
    }
}

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, string>
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _jwtService;

    public LoginUserCommandHandler(IUserRepository userRepository, ITokenService jwtService)
    {
        _userRepository = userRepository;
        _jwtService = jwtService;
    }

    public async Task<string> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetAsync(request.Email, request.Password);

        if (user == null)
        {
            throw new Exception("User not found.");
        }

        var token = _jwtService.GenerateToken(user);

        return token;
    }
}

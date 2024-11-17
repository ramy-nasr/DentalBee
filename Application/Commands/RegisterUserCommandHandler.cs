using Domain.Repositories;
using MediatR;
using Domain.Entities;
using FluentValidation;


namespace Application.Commands;

public class CreateUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required.").EmailAddress().WithMessage("A valid email is required.");
        RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required.").MinimumLength(6).WithMessage("Password must be at least 6 characters long.");
    }
}

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand>
{
    private readonly IUserRepository _userRepository;

    public RegisterUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var exists = await _userRepository.GetAsync(request.Email);

        if (exists != null)
        {
            throw new Exception("User already exists.");
        }

        var user = new User(request.Email, request.Password);

        await _userRepository.AddAsync(user);
    }
}

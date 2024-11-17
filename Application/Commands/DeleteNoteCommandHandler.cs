using Domain.Repositories;
using MediatR;
using Domain.Entities;
using FluentValidation;


namespace Application.Commands;

public class DeleteNoteCommandValidator : AbstractValidator<DeleteNoteCommand>
{
    public DeleteNoteCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Invalid Note.");
    }
}

public class DeleteNoteCommandHandler : IRequestHandler<DeleteNoteCommand>
{
    private readonly INoteRepository _noteRepository;

    public DeleteNoteCommandHandler(INoteRepository noteRepository)
    {
        _noteRepository = noteRepository;
    }

    public async Task Handle(DeleteNoteCommand request, CancellationToken cancellationToken)
    {
        var note = await _noteRepository.GetNoteAsync(request.Id);

        if (note == null)
        {
            throw new ArgumentNullException("Note is not found");
        }

        await _noteRepository.DeleteAsync(request.Id);
    }
}

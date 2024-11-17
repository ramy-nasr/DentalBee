using Domain.Repositories;
using MediatR;
using Domain.Entities;
using FluentValidation;
using System.Text;
using Infrastructure.Interfaces;
using Application.Queries;


namespace Application.Commands;

public class CreateNoteCommandValidator : AbstractValidator<CreateNoteCommand>
{
    public CreateNoteCommandValidator()
    {
        RuleFor(x => x.UserId).NotEmpty().WithMessage("Invalid User.");
        RuleFor(x => x.Title).NotEmpty().WithMessage("Title is required.");
        RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required.");
    }
}

public class CreateNoteCommandHandler : IRequestHandler<CreateNoteCommand, NoteDto>
{
    private readonly INoteRepository _noteRepository;
    private readonly IStorageSettings _storageSettings;

    public CreateNoteCommandHandler(INoteRepository noteRepository, IStorageSettings storageSettings)
    {
        _noteRepository = noteRepository;
        _storageSettings = storageSettings;
    }

    public async Task<NoteDto> Handle(CreateNoteCommand request, CancellationToken cancellationToken)
    {
        StringBuilder fileName = new();
        if (request.RecordFile != null)
        {
            fileName.Append(Guid.NewGuid());

            var filePath = Path.Combine(Path.Combine(Directory.GetCurrentDirectory(), _storageSettings.StoragePath), $"{fileName}.wav");

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await request.RecordFile.CopyToAsync(fileStream);
            }
        }

        var note = new Note(request.UserId, request.Title, request.Description, fileName.ToString());

        await _noteRepository.AddAsync(note);

        return new NoteDto
        {
            Id = note.Id,
            Description = note.Description,
            RecordName = note.RecordName,
            Title = note.Title
        };
    }
}

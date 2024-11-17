using Domain.Repositories;
using MediatR;
using Domain.Entities;
using FluentValidation;
using System.Text;
using Infrastructure.Interfaces;
using Application.Queries;


namespace Application.Commands;

public class UpdateNoteCommandValidator : AbstractValidator<UpdateNoteCommand>
{
    public UpdateNoteCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Invalid Note.");
        RuleFor(x => x.Title).NotEmpty().WithMessage("Title is required.");
        RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required.");
    }
}

public class UpdateNoteCommandHandler : IRequestHandler<UpdateNoteCommand, NoteDto>
{
    private readonly INoteRepository _noteRepository;
    private readonly IStorageSettings _storageSettings;

    public UpdateNoteCommandHandler(INoteRepository noteRepository, IStorageSettings storageSettings)
    {
        _noteRepository = noteRepository;
        _storageSettings = storageSettings;
    }

    public async Task<NoteDto> Handle(UpdateNoteCommand request, CancellationToken cancellationToken)
    {
        var note = await _noteRepository.GetNoteAsync(request.Id);

        if (note == null)
        {
            throw new ArgumentNullException("Note is not found");
        }

        StringBuilder fileName = new();
        if (request.RecordFile != null)
        {
            File.Delete(Path.Combine(Directory.GetCurrentDirectory(), _storageSettings.StoragePath, $"{note.RecordName}.wav"));
            fileName.Append(Guid.NewGuid());
            var filePath = Path.Combine(Path.Combine(Directory.GetCurrentDirectory(), _storageSettings.StoragePath), $"{fileName}.wav");

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await request.RecordFile.CopyToAsync(fileStream);
            }
        }
        else
        {
            File.Delete(Path.Combine(Directory.GetCurrentDirectory(), _storageSettings.StoragePath, $"{note.RecordName}.wav"));
        }

        note.UpdateNote(request.Title, request.Description, fileName.ToString());

        var newNote = await _noteRepository.UpdateAsync(note);

        return new NoteDto
        {
            Id = newNote.Id,
            Description = newNote.Description,
            RecordName = newNote.RecordName,
            Title = newNote.Title
        };
    }
}

using Application.Commands;
using Domain.Repositories;
using FluentValidation;
using MediatR;

namespace Application.Queries;


public class GetNotesQueryValidator : AbstractValidator<GeNotesQuery>
{
    public GetNotesQueryValidator()
    {
        RuleFor(x => x.UserId).NotEmpty().WithMessage("Invalid User.");
    }
}

public class GetNotesQueryHandler : IRequestHandler<GeNotesQuery, List<NoteDto>>
{
    private readonly INoteRepository _noteRepository;

    public GetNotesQueryHandler(INoteRepository noteRepository)
    {
        _noteRepository = noteRepository;
    }

    public async Task<List<NoteDto>> Handle(GeNotesQuery request, CancellationToken cancellationToken)
    {
        var notes = await _noteRepository.GetAllAsync(request.UserId);

        return notes.Select(note => new NoteDto
        {
            Id = note.Id,
            Description = note.Description,
            Title = note.Title,
            RecordName = note.RecordName
        }).ToList();

    }
}
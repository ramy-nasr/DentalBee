using Application.Commands;
using Application.Dto;
using Domain.Repositories;
using FluentValidation;
using Infrastructure.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Application.Queries;


public class DownloadNoteAudioQueryValidator : AbstractValidator<DownloadNoteAudioQuery>
{
    public DownloadNoteAudioQueryValidator()
    {
        RuleFor(x => x.FileName).NotEmpty().WithMessage("Invalid Record.");
    }
}

public class DownloadNoteAudioQueryHandler : IRequestHandler<DownloadNoteAudioQuery, FileStreamResult>
{
    private readonly IStorageSettings _storageSettings;

    public DownloadNoteAudioQueryHandler(IStorageSettings storageSettings)
    {
        _storageSettings = storageSettings;
    }

    public async Task<FileStreamResult> Handle(DownloadNoteAudioQuery request, CancellationToken cancellationToken)
    {
        var filePath = Path.Combine(Path.Combine(Directory.GetCurrentDirectory(), _storageSettings.StoragePath), $"{request.FileName}.wav");

        if (!File.Exists(filePath))
        {
            throw new ArgumentNullException("Record not found");
        }

        var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
        
        return await Task.FromResult(new FileStreamResult(fileStream, "audio/wav"));

    }
}
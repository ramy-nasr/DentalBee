using Application.Dto;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Application.Queries;

public class DownloadNoteAudioQuery : IRequest<FileStreamResult>
{
    public string FileName { get; set; }
}

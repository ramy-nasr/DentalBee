using Domain.Entities;

namespace Domain.Repositories;

public interface INoteRepository
{
    public Task<Note> AddAsync(Note note);
    public Task<List<Note>> GetAllAsync(string userId);
    public Task DeleteAsync(string id);
    public Task<Note> UpdateAsync(Note note);
    Task<Note> GetNoteAsync(string id);
}

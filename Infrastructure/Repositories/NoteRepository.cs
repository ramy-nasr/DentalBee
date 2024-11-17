using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Database;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

namespace Infrastructure.Repositories;

public class NoteRepository : INoteRepository
{
    private readonly IAppDbContext _dbContext;

    public NoteRepository(IAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Note> AddAsync(Note note)
    {
        var newNote = await _dbContext.Set<Note>().AddAsync(note);
        await _dbContext.SaveChangesAsync();
        return newNote.Entity;
    }

    public async Task<List<Note>> GetAllAsync(string userId)
    {
        return await _dbContext.Set<Note>().Where(note => note.UserId == userId).ToListAsync();
    }

    public async Task<Note> UpdateAsync(Note note)
    {
        var newNote = _dbContext.Set<Note>().Update(note);
        await _dbContext.SaveChangesAsync();

        return newNote.Entity;
    }

    public async Task DeleteAsync(string id)
    {
        var dbNote = await GetNoteAsync(id);

        _dbContext.Set<Note>().Remove(dbNote);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Note> GetNoteAsync(string id)
    {
        var note = await _dbContext.Set<Note>().FirstAsync(nte => nte.Id == id);
        if (note == null)
        {
            throw new InvalidOperationException("Note not found.");
        }

        return note;
    }
}
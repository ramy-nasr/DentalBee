using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Database;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

namespace Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly IAppDbContext _dbContext;

    public UserRepository(IAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(User user)
    {
        await _dbContext.Set<User>().AddAsync(user);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<User> GetAsync(string email, string password)
    {
        return await _dbContext.Set<User>().FirstOrDefaultAsync(user => user.Email == email && user.Password == password);
    }

    public async Task<User> GetAsync(string email)
    {
        return await _dbContext.Set<User>().FirstOrDefaultAsync(user => user.Email == email);
    }
}
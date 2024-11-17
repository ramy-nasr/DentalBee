using Domain.Entities;

namespace Domain.Repositories;

public interface IUserRepository
{
    Task AddAsync(User user);
    Task<User> GetAsync(string email, string password);
    Task<User> GetAsync(string email);
}

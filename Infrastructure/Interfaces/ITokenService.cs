using Domain.Entities;

namespace Infrastructure.Interfaces;

public interface ITokenService
{
    public string GenerateToken(User user);
}

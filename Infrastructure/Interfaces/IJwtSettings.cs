namespace Infrastructure.Interfaces;

public interface IJwtSettings
{
    string SecretKey { get; }
    string Issuer { get; }
    string Audience { get; }
    int ExpirationMinutes { get; }
}

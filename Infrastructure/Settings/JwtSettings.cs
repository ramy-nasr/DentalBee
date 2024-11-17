using Infrastructure.Interfaces;

namespace Infrastructure.Settings;

public class JwtSettings : IJwtSettings
{
    public string SecretKey { get; }
    public string Issuer { get; }
    public string Audience { get; }
    public int ExpirationMinutes { get; }
}

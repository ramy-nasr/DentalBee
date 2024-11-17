using Infrastructure.Interfaces;

namespace Infrastructure.Settings;

public class DbSettings : IDatabaseSettings
{
    public string ConnectionString { get; set; }
}

using Infrastructure.Interfaces;

namespace Infrastructure.Settings;

public class StorageSettings : IStorageSettings
{
    public string StoragePath { get; set; }
}
namespace WantToSell.Domain.Settings;

public static class ConfigurationSettings
{
    public static string LocalStoragePath { get; private set; }

    static ConfigurationSettings()
    {
        // string path = Path.Combine(Directory.GetCurrentDirectory(), ".storage");
        var path = Environment.GetEnvironmentVariable("FILE_STORAGE_PATH");
        try
        {
            Directory.CreateDirectory(path);
            LocalStoragePath = path;
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Failed to create local storage directory at {path}", ex);
        }
    }
}
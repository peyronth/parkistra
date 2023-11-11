namespace ParkIstra.Libraries.ASP;
public static class ConfigurationExtensions
{

    public static string Decrypt(this IConfigurationSection config)
    {
        if (string.IsNullOrEmpty(config.Value))
            return string.Empty;

        return AESProvider.Decrypt(config.Value);
    }
    public static IConfigurationSection DecryptSections(this IConfigurationSection config, bool isDevelopment, string[] sectionNames)
    {
        if (isDevelopment)
            return config;

        foreach (string name in sectionNames)
        {
            var section = config.GetSection(name);
            if (section != null)
                section.Value = section.Decrypt();
        }
        return config;
    }
}

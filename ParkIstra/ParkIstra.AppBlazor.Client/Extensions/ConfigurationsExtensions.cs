using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration.Memory;

namespace ParkIstra.AppBlazor.Client.Extensions;

public static class ConfigurationsExtensions
{
    public static async Task<WebAssemblyHostConfiguration> AddConfigurationsAsync(
        this WebAssemblyHostConfiguration configuration,
        HttpClient httpClient)
    {
        using var response = await httpClient.GetAsync("config.json");
        using var stream = await response.Content.ReadAsStreamAsync();

        _ = configuration.AddJsonStream(stream);
        _ = configuration.Add(GetMemoryConfigurationSource());

        return configuration;
    }

    private static MemoryConfigurationSource GetMemoryConfigurationSource()
    {
        var config = new Dictionary<string, string>()
            {
                { "UsingMemoryConfigurationSource", "true" },
                { "IsBadAddressMenuDisabled", "true" },
            };

        return new MemoryConfigurationSource { InitialData = config };
    }
}

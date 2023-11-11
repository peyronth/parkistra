namespace ParkIstra.Services.MainApi.Extensions;

public static class HstsExtensions
{
    public static IServiceCollection AddHstsService(this IServiceCollection services) =>
        services.AddHsts(options =>
        {
            options.Preload = true;
            options.IncludeSubDomains = true;
            //options.MaxAge = TimeSpan.FromDays(30);
            //options.ExcludedHosts.Add("example.com");
            //options.ExcludedHosts.Add("www.example.com");
        });
}

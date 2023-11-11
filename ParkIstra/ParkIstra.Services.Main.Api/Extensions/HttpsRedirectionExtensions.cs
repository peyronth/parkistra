namespace ParkIstra.Services.MainApi.Extensions;

public static class HttpsRedirectionExtensions
{
    public static IServiceCollection AddHttpsRedirectionService(
        this IServiceCollection services,
        bool isDevelopment)
    {
        if (!isDevelopment)
        {
            _ = services.AddHttpsRedirection(options =>
            {
                //options.RedirectStatusCode = (int)HttpStatusCode.TemporaryRedirect; // default
                //options.HttpsPort = ...; // use configured https port
            });
        }

        return services;
    }
}

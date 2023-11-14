namespace ParkIstra.Services.MainApi.Extensions;

public static class CorsExtensions
{
    public static IServiceCollection AddCorsService(
        this IServiceCollection services,
        string policyName)
    {
        return services.AddCors(options =>
        {
            options.AddPolicy(policyName, builder =>
            {
                builder
                    .AllowAnyOrigin()
                    //.WithOrigins(
                    //    "http://example.com",
                    //    "https://localhost:44387")
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
        });
    }
}

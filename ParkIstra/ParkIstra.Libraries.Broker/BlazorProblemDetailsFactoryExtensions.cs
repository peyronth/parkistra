namespace ParkIstra.Libraries.Blazor;
public static class BlazorProblemDetailsFactoryExtensions
{
    public static IServiceCollection AddBlazorProblemDetailsFactory(
        this IServiceCollection services,
        bool isDevelopment) =>
        services.AddTransient<BlazorProblemDetailsFactory>()
            .Configure<BlazorProblemOptions>(options =>
                options.IsDetailed = isDevelopment);
}

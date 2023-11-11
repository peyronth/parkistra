namespace ParkIstra.Libraries.ASP;

public static class ASPProblemDetailsFactoryExtensions
{
    public static IServiceCollection AddASPProblemDetailsFactory(
        this IServiceCollection services,
        bool isDetailed)
    {
        _ = services.AddTransient<ProblemDetailsFactory, ASPProblemDetailsFactory>()
            .Configure<ASPApiBehaviorOptions>(options =>
                options.IsDetailed = isDetailed);

        return services.AddTransient<ASPProblemDetailsFactory>()
            .Configure<ASPApiBehaviorOptions>(options =>
                options.IsDetailed = isDetailed);
    }
}

namespace ParkIstra.Contexts.Util;

public static class UtilDbContextExtensions
{
    public static IServiceCollection AddUtilDbContext(this IServiceCollection services,
        bool isDevelopment,
        IConfiguration configuration)
    {
        var connectionString = configuration["ConnectionStrings:PartialMini"];

        return services.AddDbContextFactory<UtilDbContext>(
            (serviceProvider, options) => options
                .ConfigureWarnings(warnings =>
                    warnings.Ignore(
                        CoreEventId.SensitiveDataLoggingEnabledWarning,
                        CoreEventId.RedundantIndexRemoved))
                .UseSqlServer(connectionString,
                    sqlServerOptionsAction => sqlServerOptionsAction
                        .UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)
                        .CommandTimeout(5))
                );
    }
}

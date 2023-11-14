namespace ParkIstra.Contexts.Main;

public static class MainDbContextExtensions
{
    public static IServiceCollection AddMainDbContext(this IServiceCollection services,
        bool isDevelopment,
        IConfiguration configuration)
    {
        var connectionString = configuration["ConnectionStrings:PartialMiniDb"];

        return services.AddDbContextFactory<MainDbContext>(
            (serviceProvider, options) => options
                .ConfigureWarnings(warnings =>
                    warnings.Ignore(
                        CoreEventId.SensitiveDataLoggingEnabledWarning,
                        CoreEventId.RedundantIndexRemoved))
                .UseSqlServer(connectionString,
                    sqlServerOptionsAction => sqlServerOptionsAction
                        .UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)
                        .CommandTimeout(5)
                        .MigrationsAssembly("ParkIstra.Services.MainApi")) // Specify the migrations assembly here
                );
    }
}
namespace ParkIstra.Services.MainApi.Extensions
{
    public static class PolicyExtensions
    {
        public static IServiceCollection AddPolicyExtensions(this IServiceCollection Services)
        {
            Services.AddAuthorization(options =>
            {
                options.AddPolicy("Project.Read", policy => policy.RequireClaim("Permission", "Project.Read"));
            });

            return Services;
        }

    }
}

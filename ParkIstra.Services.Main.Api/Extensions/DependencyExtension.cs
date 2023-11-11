using ParkIstra.Services.EmailsSender;

namespace ParkIstra.Services.MainApi.Extensions
{
    public static class DependencyExtensions
    {
        public static IServiceCollection AddDependencyExtensions(this IServiceCollection Services)
        {
            Services.AddTransient<IEmailSender, EmailSender>();

            Services.AddHttpContextAccessor();

            return Services;
        }

    }
}

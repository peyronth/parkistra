using ParkIstra.Services.EmailsSender;
using System.Security.Claims;

namespace ParkIstra.Services.MainApi.Extensions
{
    public static class EmailClientExtensions
    {
        public static IServiceCollection AddEmailClientExtensions(this IServiceCollection Services, IConfiguration Configuration)
        {
            var emailConfig = Configuration
                    .GetSection("EmailConfiguration")
                    .Get<EmailConfiguration>();

            return Services.AddSingleton(emailConfig);
        }

    }
}

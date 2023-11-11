using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json.Linq;
using System.Data;

namespace ParkIstra.Services.MainApi.Extensions
{
    public static class IdentityExtensions
    {
        public static IServiceCollection AddIdentityExtensions(this IServiceCollection Services)
        {
            Services.AddIdentity<ApplicationUser, ApplicationRole>()
                    .AddRoles<ApplicationRole>()
                    .AddEntityFrameworkStores<MainDbContext>()
                    .AddDefaultTokenProviders();

            Services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.User.RequireUniqueEmail = true;
                options.Tokens.AuthenticatorIssuer = "JWT";

                options.SignIn.RequireConfirmedEmail = true;

            });


            return Services;
        }

    }
}

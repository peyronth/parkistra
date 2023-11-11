using ParkIstra.Models.Shared;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ParkIstra.Services.MainApi.Extensions
{
    public static class JWTExtensions
    {
        public static IServiceCollection AddJWTExtensions(this IServiceCollection Services, IConfiguration Configuration, string env)
        {

            var jwtConfiguration = Configuration
            .GetSection("JwtConfiguration")
            .Get<JwtConfiguration>();

            Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = jwtConfiguration.ValidAudience,
                    ValidIssuer = jwtConfiguration.ValidIssuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfiguration.Secret))
                };
            });
            Services.AddSingleton(jwtConfiguration);

            return Services;
        }
    }
        
}

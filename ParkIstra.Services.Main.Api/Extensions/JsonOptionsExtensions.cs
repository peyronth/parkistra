using System.Text.Encodings.Web;
using System.Text.Json.Serialization;

namespace ParkIstra.Services.MainApi.Extensions;

public static class JsonOptionsExtensions
{
    public static IServiceCollection AddJsonOptionsConfiguration(this IServiceCollection services)
    {
        // exactly what services.AddControllers().AddJsonOptions(...) do !
        _ = services.Configure<Microsoft.AspNetCore.Mvc.JsonOptions>(options =>
            {
                //options.SerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
                //options.SerializerOptions.PropertyNameCaseInsensitive = true;
                //options.SerializerOptions.NumberHandling = JsonNumberHandling.AllowReadingFromString;
                options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                options.JsonSerializerOptions.WriteIndented = true;
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                options.JsonSerializerOptions.Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
            });

        return services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
        {
            //options.SerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
            //options.SerializerOptions.PropertyNameCaseInsensitive = true;
            //options.SerializerOptions.NumberHandling = JsonNumberHandling.AllowReadingFromString;
            options.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            options.SerializerOptions.WriteIndented = true;
            options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            options.SerializerOptions.Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
        });
    }
}

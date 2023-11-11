using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ParkIstra.AppBlazor.Client.Extensions;

public static class JsonSerializerOptionsExtensions
{
    public static IServiceCollection AddJsonSerializerOptions(this IServiceCollection services) =>
        services.AddSingleton(_ => new JsonSerializerOptions(JsonSerializerDefaults.Web)
        {
            //PropertyNameCaseInsensitive = true,
            //PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            //NumberHandling = JsonNumberHandling.AllowReadingFromString,
            WriteIndented = true,
            ReferenceHandler = ReferenceHandler.IgnoreCycles,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
        });
}

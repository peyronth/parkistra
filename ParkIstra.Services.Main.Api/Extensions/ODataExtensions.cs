using Microsoft.AspNetCore.OData;

namespace ParkIstra.Services.MainApi.Extensions;

public static class ODataExtensions
{
    public static IMvcBuilder AddODataService(this IMvcBuilder builder)
    {
        return builder.AddOData(options =>
        {
            _ = options.Filter().Expand().Select().OrderBy().Count().SetMaxTop(50);
        });
    }
}

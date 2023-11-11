namespace ParkIstra.Libraries.Blazor;

public class BlazorProblemDetailsFactory
{
    public BlazorProblemDetailsFactory(IOptions<BlazorProblemOptions> options)
    {
        Options = options?.Value ?? throw new ArgumentNullException(nameof(options));
    }

    public BlazorProblemDetails CreateBlazorProblemDetails(Exception exception)
    {
        var blazorProblemDetails = new BlazorProblemDetails
        {
            Type = exception.GetType().Name,
            Title = exception.Message,
            Detail = Options.IsDetailed ? exception.StackTrace : null,
            Status = 503, // demo purpose - 503 Service Unavailable
            Instance = "Blazor_Handled_Exception",
            TraceId = Options.IsTraceIdIncluded
                ? Activity.Current?.Id ?? Guid.NewGuid().ToString()
                : null
        };

        if (exception is NotSupportedException)
        {
            blazorProblemDetails.Status = 500;
            blazorProblemDetails.Instance = "Blazor_Handled_NotSupportedException";
        }

        if (exception is TaskCanceledException)
        {
            blazorProblemDetails.Status = 500;
            blazorProblemDetails.Instance = "Blazor_Handled_TaskCanceledException";
        }

        if (exception is HttpRequestException requestException)
        {
            blazorProblemDetails.Status = Convert.ToInt32(requestException.StatusCode);
            blazorProblemDetails.Instance = "Blazor_Handled_HttpRequestException";
        }

        return blazorProblemDetails;
    }
    private BlazorProblemOptions Options { get; init; }
}

using ProblemDetailsFactory = Microsoft.AspNetCore.Mvc.Infrastructure.ProblemDetailsFactory;

using Hellang.Middleware.ProblemDetails;

namespace ParkIstra.Libraries.ASP;

public partial class ASPProblemDetailsFactory : ProblemDetailsFactory
{
    public ASPProblemDetailsFactory(IOptions<ASPApiBehaviorOptions> options)
    {
        Options = options?.Value ?? throw new ArgumentNullException(nameof(options));
    }

    public override ProblemDetails CreateProblemDetails(
        HttpContext? httpContext = null,
        int? statusCode = null,
        string? title = null,
        string? type = null,
        string? detail = null,
        string? instance = null)
    {
        httpContext ??= ASPHttpContext;
        statusCode ??= 500;

        var problemDetails = new ProblemDetails
        {
            Status = statusCode,
            Title = title,
            Type = type,
            Detail = detail,
            Instance = instance,
        };

        ApplyProblemDetailsDefaults(httpContext, problemDetails, statusCode.Value);

        return problemDetails;
    }

    public override ValidationProblemDetails CreateValidationProblemDetails(
        HttpContext? httpContext = null,
        ModelStateDictionary modelStateDictionary = null!,
        int? statusCode = null,
        string? title = null,
        string? type = null,
        string? detail = null,
        string? instance = null)
    {
        ArgumentNullException.ThrowIfNull(nameof(modelStateDictionary));

        httpContext ??= ASPHttpContext;
        statusCode ??= 400;

        var problemDetails = new ValidationProblemDetails(modelStateDictionary)
        {
            Status = statusCode,
            Type = type,
            Detail = detail,
            Instance = instance,
        };

        if (!Options.IsDetailed) problemDetails.Instance = null;
        problemDetails.Instance ??= nameof(ValidationProblemDetails);

        if (Options.IsDetailed && title != null)
        { problemDetails.Title = title; }// don't overwrite the default title with null.

        ApplyProblemDetailsDefaults(httpContext, problemDetails, statusCode.Value);

        return problemDetails;
    }

    public ProblemDetails CreateProblem(
        Exception? exception = null,
        int? statusCode = null,
        string? instance = null)
    {
        var title = exception?.Message;
        var detail = exception?.StackTrace;

        return CreateProblemDetails(null, statusCode, title, null, detail, instance);
    }

    private void ApplyProblemDetailsDefaults(HttpContext httpContext, ProblemDetails problemDetails, int statusCode)
    {
        problemDetails.Status ??= statusCode;

        if (!Options.IsDetailed)
        {
            problemDetails.Detail = null;
            if (problemDetails.Instance != nameof(ValidationProblemDetails))
            {
                problemDetails.Title = null;
                problemDetails.Instance = null;
            }
        }

        problemDetails.Instance ??= nameof(ProblemDetails);

        if (ProblemDetailsDefaults.Defaults.TryGetValue(statusCode, out var defaults))
        {
            problemDetails.Title ??= defaults.Title;
            problemDetails.Type ??= defaults.Type;
        }

        if (Options.IsTraceIdIncluded)
        {
            httpContext ??= ASPHttpContext;
            problemDetails.Extensions["traceId"] = Activity.Current?.Id ?? httpContext.TraceIdentifier;
        }
    }

    private HttpContext ASPHttpContext =>
        new DefaultHttpContext { TraceIdentifier = Guid.NewGuid().ToString() };
    private ASPApiBehaviorOptions Options { get; init; }
}

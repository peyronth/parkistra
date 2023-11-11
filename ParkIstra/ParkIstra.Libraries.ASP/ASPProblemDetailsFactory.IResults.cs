namespace ParkIstra.Libraries.ASP;

public partial class ASPProblemDetailsFactory
{
    public IResult ResultsBadRequest(string? message = null) =>
        Results.BadRequest(CreateProblemDetails(
            statusCode: StatusCodes.Status400BadRequest,
            title: message ?? "Bad Request"));

    public IResult ResultsNotFound(string? message = null) =>
        Results.NotFound(CreateProblemDetails(
            statusCode: StatusCodes.Status404NotFound,
            title: message ?? "Not Found"));

    public IResult ResultsValidationProblem(IDictionary<string, string[]> errors)
    {
        ModelStateDictionary modelStateDictionary = new();
        errors.ToList().ForEach(e =>
        {
            var key = e.Key;
            e.Value.ToList().ForEach(v => modelStateDictionary.AddModelError(key, v));
        });

        return Results.Problem(CreateValidationProblemDetails(
            modelStateDictionary: modelStateDictionary));
    }
}

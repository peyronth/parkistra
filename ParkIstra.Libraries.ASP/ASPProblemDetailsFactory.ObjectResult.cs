namespace ParkIstra.Libraries.ASP;

public partial class ASPProblemDetailsFactory
{
    public ObjectResult ObjectResultBadRequest(string? message = null) =>
        new(CreateProblemDetails(
            statusCode: StatusCodes.Status400BadRequest,
            title: message ?? "Bad Request"));

    public ObjectResult ObjectResultNotFound(string? message = null) =>
        new(CreateProblemDetails(
            statusCode: StatusCodes.Status404NotFound,
            title: message ?? "Not Found"));

    public ObjectResult ObjectResultValidationProblem(IDictionary<string, string[]> errors)
    {
        Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary modelStateDictionary = new();
        errors.ToList().ForEach(e =>
        {
            var key = e.Key;
            e.Value.ToList().ForEach(v => modelStateDictionary.AddModelError(key, v));
        });

        return new(CreateValidationProblemDetails(
            modelStateDictionary: modelStateDictionary));
    }
}

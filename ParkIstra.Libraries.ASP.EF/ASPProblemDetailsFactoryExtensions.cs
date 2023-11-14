namespace ParkIstra.Libraries.ASP.EF;

public static partial class ASPProblemDetailsFactoryExtensions
{
    public static IResult ResultsFailedEFSave(
        this ASPProblemDetailsFactory problemFactory,
        EFSaveResult saveResult)
    {
        if (saveResult.Exception is EntityValidationException)
        {
            var validationProblemDetails = CreateValidationProblem(
                problemFactory, saveResult);

            return Results.Problem(validationProblemDetails);
        }

        if (saveResult.Exception is null || saveResult.Exception is DbUpdateConcurrencyException)
        {
            var zeroRowsProblemDetails = CreateProblem(
                problemFactory, StatusCodes.Status404NotFound, saveResult);

            return Results.NotFound(zeroRowsProblemDetails);
        }

        var problemDetails = CreateProblem(
            problemFactory, StatusCodes.Status400BadRequest, saveResult);

        return Results.Problem(problemDetails);
    }

    public static ObjectResult ObjectResultFailedEFSave(
        this ASPProblemDetailsFactory problemFactory,
        EFSaveResult saveResult)
    {
        if (saveResult.Exception is EntityValidationException)
        {
            var validationProblemDetails = CreateValidationProblem(
                problemFactory, saveResult);

            return new(validationProblemDetails);
        }

        if (saveResult.Exception is null || saveResult.Exception is DbUpdateConcurrencyException)
        {
            var zeroRowsProblemDetails = CreateProblem(
                problemFactory, StatusCodes.Status404NotFound, saveResult);

            return new(zeroRowsProblemDetails);
        }

        var problemDetails = CreateProblem(
            problemFactory, StatusCodes.Status400BadRequest, saveResult);

        return new(problemDetails);
    }

    private static ProblemDetails CreateValidationProblem(
        ASPProblemDetailsFactory problemFactory,
        EFSaveResult saveResult)
    {
        ModelStateDictionary modelStateDictionary = new();
        saveResult.Errors!.ToList().ForEach(e =>
        {
            var key = e.Key;
            e.Value.ToList().ForEach(v => modelStateDictionary.AddModelError(key, v));
        });

        return problemFactory.CreateValidationProblemDetails(
            modelStateDictionary: modelStateDictionary,
            title: saveResult.Title,
            detail: saveResult.Exception?.StackTrace,
            instance: saveResult.Instance);
    }

    private static ProblemDetails CreateProblem(
        ASPProblemDetailsFactory problemFactory,
        int statusCodes,
        EFSaveResult saveResult)
    {
        return problemFactory.CreateProblemDetails(
            statusCode: statusCodes,
            title: saveResult.Title,
            detail: saveResult.Exception?.StackTrace,
            instance: saveResult.Instance);
    }
}

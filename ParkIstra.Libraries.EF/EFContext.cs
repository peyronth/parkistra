namespace ParkIstra.Libraries.EF;

public class EFContext
{
    public static async Task<EFSaveResult> InvokeAsync(Func<Task<int>> efSaveFunc)
    {
        try
        { return GetEFSaveResult(await efSaveFunc()); }
        catch (Exception exception)
        { return GetEFSaveResult(exception); }
    }

    public static EFSaveResult Invoke(Func<int> efSaveFunc)
    {
        try
        { return GetEFSaveResult(efSaveFunc()); }
        catch (Exception exception)
        { return GetEFSaveResult(exception); }
    }

    private static EFSaveResult GetEFSaveResult(int rowsAffected)
    {
        if (rowsAffected != 0)
        { return new() { IsSuccess = true, RowsAffected = rowsAffected }; }

        return new()
        {
            Title = "DbContext operation affected an unexpected number (0) of rows.",
            Instance = "EFContext_Handled_NoException"
        };
    }

    private static EFSaveResult GetEFSaveResult(Exception exception)
    {
        var efSaveResult = new EFSaveResult();

        switch (exception)
        {
            case EntityValidationException entityValidationException:
                {
                    efSaveResult.Exception = entityValidationException.GetBaseException();
                    efSaveResult.Title = entityValidationException.Message;
                    efSaveResult.Instance = "EFContext_Handled_EntityValidationException";
                    efSaveResult.Errors = entityValidationException.GetErrors();
                    break;
                }
            case DbUpdateConcurrencyException:
                {
                    efSaveResult.Exception = exception.GetBaseException();
                    efSaveResult.Title = efSaveResult.Exception.Message;
                    efSaveResult.Instance = "EFCore_Handled_DbUpdateConcurrencyException";
                    break;
                }
            case DbUpdateException:
                {
                    efSaveResult.Exception = exception.GetBaseException();
                    efSaveResult.Title = efSaveResult.Exception.Message;
                    efSaveResult.Instance = "SqlServer_Handled_DbUpdateException";
                    break;
                }
            case InvalidOperationException:
                {
                    efSaveResult.Exception = exception.GetBaseException();
                    efSaveResult.Title = efSaveResult.Exception.Message;
                    efSaveResult.Instance = "EFCore_Handled_InvalidOperationException";
                    break;
                }
            default:
                {
                    efSaveResult.Exception = exception;
                    efSaveResult.Title = exception.Message;
                    efSaveResult.Instance = $"Handled_{exception.GetType().Name}";
                    break;
                }
        }

        return efSaveResult;
    }

    public static EFModifyResult Invoke(Func<EntityEntry> efCommand, object validationEntity = null!)
    {
        try
        {
            if (validationEntity is not null)
            { _ = EFValidator.Validate(validationEntity); }

            return new EFModifyResult { IsSuccess = true, EntityEntry = efCommand() };
        }
        catch (Exception exception)
        { return GetEFModifyResult(exception); }
    }

    public static EFModifyResult Invoke(Action efCommand, IEnumerable<object> validationCollection = null!)
    {
        try
        {
            if (validationCollection is not null)
            {
                foreach (var entity in validationCollection)
                { _ = EFValidator.Validate(entity); }
            }

            efCommand();
            return new EFModifyResult { IsSuccess = true };
        }
        catch (Exception exception)
        { return GetEFModifyResult(exception); }
    }

    private static EFModifyResult GetEFModifyResult(Exception exception)
    {
        var efModifyResult = new EFModifyResult();

        switch (exception)
        {
            case EntityValidationException entityValidationException:
                {
                    efModifyResult.Exception = entityValidationException.GetBaseException();
                    efModifyResult.Title = entityValidationException.Message;
                    efModifyResult.Instance = "EFContext_Handled_EntityValidationException";
                    efModifyResult.Errors = entityValidationException.GetErrors();
                    break;
                }
            case InvalidOperationException:
                {
                    efModifyResult.Exception = exception.GetBaseException();
                    efModifyResult.Title = efModifyResult.Exception.Message;
                    efModifyResult.Instance = "EFCore_Handled_InvalidOperationException";
                    break;
                }
            default:
                {
                    efModifyResult.Exception = exception;
                    efModifyResult.Title = exception.Message;
                    efModifyResult.Instance = $"Handled_{exception.GetType().Name}";
                    break;
                }
        }

        return efModifyResult;
    }
}

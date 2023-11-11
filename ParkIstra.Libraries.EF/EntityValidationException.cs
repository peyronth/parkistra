namespace ParkIstra.Libraries.EF;

public class EntityValidationException : DataException
{
    public EntityValidationException() : base()
    { }
    public EntityValidationException(
        string message) : base(message)
    { }
    public EntityValidationException(
        string message,
        IEnumerable<ValidationResult> entityValidationResults) : base(message)
    { EntityValidationResults = entityValidationResults.ToList(); }
    public EntityValidationException(
        string message,
        Exception innerException) : base(message, innerException)
    { }
    public EntityValidationException(
        string message,
        IEnumerable<ValidationResult> entityValidationResults,
        Exception innerException) : base(message, innerException)
    { EntityValidationResults = entityValidationResults.ToList(); }

    public IDictionary<string, string[]>? GetErrors()
    {
        if (EntityValidationResults.Count == 0) return null;

        return EntityValidationResults
            .Select(vr => new
            {
                MemberName = vr.MemberNames.First(),
                Message = vr.ErrorMessage ?? String.Empty,
            })
            .GroupBy(gg => gg.MemberName)
            .ToDictionary(gg => gg.Key, gg => gg.Select(gg => gg.Message).ToArray());
    }

    public List<ValidationResult> EntityValidationResults { get; } = new();
}

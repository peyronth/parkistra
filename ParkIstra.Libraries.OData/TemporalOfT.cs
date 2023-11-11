namespace ParkIstra.Libraries.OData;

public class Temporal<T> where T : class
{
    public T Entity { get; init; } = default!;
    public DateTime? From { get; init; }
    public DateTime? To { get; init; }
}

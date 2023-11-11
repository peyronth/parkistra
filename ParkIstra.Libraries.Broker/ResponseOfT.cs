namespace ParkIstra.Libraries.Blazor;
public record Response<T> where T : class
{
    public bool IsSuccess { get; set; } = true;
    public List<T>? Many { get; set; }
    public T? Single { get; set; }
    public BlazorProblemDetails? BlazorProblemDetails { get; set; }

    //Za potrebe FileStream-a
    public Stream Stream { get; set; }
}

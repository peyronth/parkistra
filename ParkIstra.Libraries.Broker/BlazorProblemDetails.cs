namespace ParkIstra.Libraries.Blazor;

public record BlazorProblemDetails
{
    public string? Type { get; init; }
    public string? Title { get; init; }
    public int? Status { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public HttpStatusCode? HttpStatusCode => Status != null ? (HttpStatusCode)Status : null;
    public string? Detail { get; init; }
    public string? Instance { get; set; }
    public string? TraceId { get; init; }
    public IDictionary<string, List<string>>? Errors { get; init; }
}

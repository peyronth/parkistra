namespace ParkIstra.Libraries.EF;

public record EFResult()
{
    public bool IsSuccess { get; set; }
    public string? Title { get; set; }
    public string? Instance { get; set; }
    public IDictionary<string, string[]>? Errors { get; set; }
}

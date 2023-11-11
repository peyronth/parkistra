namespace ParkIstra.Libraries.EF;

public record EFModifyResult : EFResult
{
    public EntityEntry? EntityEntry { get; set; }
    public Exception? Exception { get; set; }
}

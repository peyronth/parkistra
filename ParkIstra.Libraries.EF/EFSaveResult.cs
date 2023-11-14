namespace ParkIstra.Libraries.EF;

public record EFSaveResult : EFResult
{
    public int RowsAffected { get; set; }
    public Exception? Exception { get; set; }
}

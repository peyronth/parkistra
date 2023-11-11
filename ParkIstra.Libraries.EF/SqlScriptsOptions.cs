namespace ParkIstra.Libraries.EF;

public record SqlScriptsOptions
{
    public string FolderName { get; init; } = string.Empty;
    public List<string> DropCreateDbFileNames { get; init; } = new();
    public List<string> DDLFileNames { get; init; } = new();
    public List<string> SeedDataFileNames { get; init; } = new();
}

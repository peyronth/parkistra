namespace ParkIstra.Libraries.EF;

public interface IEntityTypeCorrector
{
    public Type Type { get; }

    bool EnsureCorrected(EntityEntry entityEntry);
}

using ParkIstra.AppInterfaces;

namespace ParkIstra.AppBlazor.Client.Infrastructure;

public class StateContainer : IStateContainer
{
    public Action? OnChanged { get; set; }
    public bool IsMovieThumbnailVisible { get; set; }
    public string MovieTitleFilter { get; set; } = String.Empty;
    public string MovieDirectorFilter { get; set; } = String.Empty;

    public void HasChanged(string propertyName, object value)
    {
        GetType().GetProperty(propertyName)?.SetValue(this, value);
        NotifyStateChanged();
    }

    public object GetValue(string propertyName)
    { return GetType().GetProperty(propertyName)?.GetValue(this) ?? ""; }

    private void NotifyStateChanged() => OnChanged?.Invoke();
}

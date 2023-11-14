namespace ParkIstra.AppInterfaces;

public interface IStateContainer
{
    Action? OnChanged { get; set; }
    void HasChanged(string propertyName, object value);
    object GetValue(string propertyName);
}
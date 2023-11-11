namespace ParkIstra.Libraries.Blazor.Components;

public record DialogOptions
{
    public string DialogTitle { get; set; } = "Dialog Title";
    public string DialogMessage { get; set; } = "Dialog message?";
    public string OkButtonText { get; set; } = "OK";
    public string CancelButtonText { get; set; } = "Cancel";
    public DialogType DialogType { get; set; } = DialogType.Normal;
}

namespace ParkIstra.Libraries.Blazor.Components;

public partial class Filter
{
    [Parameter]
    public string Entity { get; set; } = string.Empty;
    [Parameter]
    public string Property { get; set; } = string.Empty;
    [Parameter]
    public int Width { get; set; } = 120;
    [Parameter]
    public bool IsEntityNameVisible { get; set; }
    [Parameter]
    public bool IsPropertyNameVisible { get; set; }

    public async Task FocusAsync()
    { await FilterInput.FocusAsync(); }

    private ElementReference FilterInput { get; set; }
    private string Title => $"{(IsEntityNameVisible ? ($"{Entity} ") : "")}{(IsPropertyNameVisible ? Property : "")}";
    private string WidthCssProperty => $"{Width.ToString(CultureInfo.InvariantCulture)}px";
    private string Value { get; set; } = string.Empty;

    protected override void OnInitialized()
    {
        Value = StateContainer.GetValue($"{Entity}{Property}Filter")?.ToString() ?? "";
        _systemTimer = new(400);
        _systemTimer.Stop();
        _systemTimer.Elapsed += SystemTimerElapsed;
        _systemTimer.AutoReset = false;
    }

    private void OnValueInputted(ChangeEventArgs e)
    { Value = e.Value?.ToString() ?? String.Empty; }

    private void OnValueKeyUp(KeyboardEventArgs e)
    {
        _systemTimer.Stop();
        _systemTimer.Start();
    }

    private void SystemTimerElapsed(object? sender, System.Timers.ElapsedEventArgs e)
    { ValueChanged(); }

    private void ValueChanged()
    { StateContainer.HasChanged($"{Entity}{Property}Filter", Value); }

    private async Task ClearFilterAsync()
    {
        Value = String.Empty;
        ValueChanged();
        await FocusAsync();
    }

    [AllowNull]
    private System.Timers.Timer _systemTimer;

    [Inject, AllowNull]
    private IStateContainer StateContainer { get; set; }
}

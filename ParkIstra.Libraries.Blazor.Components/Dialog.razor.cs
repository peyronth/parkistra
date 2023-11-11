namespace ParkIstra.Libraries.Blazor.Components;

public partial class Dialog
{
    [Parameter, AllowNull]
    public EventCallback<bool> OnClosed { get; set; }

    [Parameter]
    public EventCallback<Task> OnClickCallback { get; set; }

    [JSInvokable]
    public async Task JsInvokeEscPressedAsync() => await CloseAsync(false);

    public async Task ShowAsync(DialogOptions? options = null)
    {
        if (options != null)
        {
            if (options.DialogTitle != null) DialogTitle = options.DialogTitle;
            if (options.DialogMessage != null) DialogMessage = options.DialogMessage;
            if (options.OkButtonText != null) OkButtonText = options.OkButtonText;
            if (options.CancelButtonText != null) CancelButtonText = options.CancelButtonText;

            if (options.DialogType == DialogType.Informational)
            {
                OkButtonCssClass = "d-none";
                CancelButtonText = OkButtonText;
                CancelButtonCssClass = "btn btn-outline-primary mr-3";
            }
            else if (options.DialogType == DialogType.Dangerous)
            { OkButtonCssClass = "btn btn-outline-danger mr-3"; }

            Options = options;
        }

        IsVisible = true;
        await JSRuntime.InvokeVoidAsync("blazorInterop.registerEscListener", DotNetObjectReference.Create(this));
        await InvokeAsync(StateHasChanged);
    }

    private bool IsVisible { get; set; }
    private string DialogTitle { get; set; } = "Dialog Title";
    private string DialogMessage { get; set; } = "Dialog message?";
    private string OkButtonText { get; set; } = "OK";
    private string CancelButtonText { get; set; } = "Cancel";
    private string OkButtonCssClass { get; set; } = "btn btn-outline-primary mr-3";
    private string CancelButtonCssClass { get; set; } = "btn btn-outline-secondary mr-3";
    private ElementReference OkButton { get; set; }
    private ElementReference CancelButton { get; set; }
    private DialogOptions? Options { get; set; }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (IsVisible)
        {
            if (Options is not null && Options.DialogType != DialogType.Normal)
            { await CancelButton.FocusAsync(); }
            else
            { await OkButton.FocusAsync(); }
        }
    }

    private async Task CloseAsync(bool value)
    {
        IsVisible = false;
        await OnClosed.InvokeAsync(value);
        await JSRuntime.InvokeVoidAsync("blazorInterop.unRegisterEscListener");
        await InvokeAsync(StateHasChanged);
        await OnClickCallback.InvokeAsync();
    }

    [Inject, AllowNull]
    private IJSRuntime JSRuntime { get; set; }
}

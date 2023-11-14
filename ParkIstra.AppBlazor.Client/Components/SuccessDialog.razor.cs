using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Diagnostics.CodeAnalysis;

namespace ParkIstra.AppBlazor.Client.Components;

public partial class SuccessDialog
{

    [Parameter, AllowNull]
    public EventCallback<bool> OnClosed { get; set; }

    [Parameter]
    public EventCallback<Task> OnClickCallback { get; set; }

    [JSInvokable]
    public async Task JsInvokeEscPressedAsync() => await CloseAsync(false);

    public async Task ShowAsync(SuccessDialogOptions? options = null)
    {
        if (options != null)
        {
            if (options.DialogTitle != null) DialogTitle = options.DialogTitle;
            if (options.DialogMessage != null) DialogMessage = options.DialogMessage;
            if (options.OkButtonText != null) OkButtonText = options.OkButtonText;
            if (options.CancelButtonText != null) CancelButtonText = options.CancelButtonText;

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
    private SuccessDialogOptions? Options { get; set; }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (IsVisible)
        {
            if (Options is not null && Options.DialogType != SucessDialogType.Normal)
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

    private async Task OkClicked()
    {
        NavManager.NavigateTo($"/manage/{Options?.NavigationId}", true);
    }

    [Inject, AllowNull]
    private IJSRuntime JSRuntime { get; set; }

    [Inject, AllowNull]
    NavigationManager NavManager { get; set; }
}
public record SuccessDialogOptions
{
    public string DialogTitle { get; set; } = "Dialog Title";
    public string DialogMessage { get; set; } = "Dialog message?";
    public string OkButtonText { get; set; } = "OK";
    public string CancelButtonText { get; set; } = "Cancel";
    public SucessDialogType DialogType { get; set; } = SucessDialogType.Normal;

    public int NavigationId { get; set; }


}
public enum SucessDialogType : int
{
    Normal,
    Informational,
    Dangerous
}

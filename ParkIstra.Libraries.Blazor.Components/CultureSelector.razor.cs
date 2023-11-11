namespace ParkIstra.Libraries.Blazor.Components;

public partial class CultureSelector
{
    private CultureInfo[] SupportedCultures => new[]
    {
        new CultureInfo("en-US"),
        new CultureInfo("sr-Latn-BA"),
        new CultureInfo("sr-Cyrl-BA"),//-RS za srpski
        new CultureInfo("hr-BA"),
    };
    private CultureInfo Culture
    {
        get => CultureInfo.CurrentCulture;
        set
        {
            if (CultureInfo.CurrentCulture != value)
            {
                var js = (IJSInProcessRuntime)JSRuntime;
                js.InvokeVoid("blazorInterop.applicationCulture.set", value.Name);

                NavigationManager.NavigateTo(NavigationManager.Uri, forceLoad: true);
            }
        }
    }

    [Inject, AllowNull]
    private NavigationManager NavigationManager { get; set; }
    [Inject, AllowNull]
    private IJSRuntime JSRuntime { get; set; }
}

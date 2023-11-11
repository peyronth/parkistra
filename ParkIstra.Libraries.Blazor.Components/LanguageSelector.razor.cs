namespace ParkIstra.Libraries.Blazor.Components;
public class LanguageSelectorBase : ComponentBase
{
    [Inject]
    IJSRuntime JSRuntime { get; set; }
    [Inject]
    public NavigationManager NavigationManager { get; set; }

    protected Dictionary<CultureInfo, string> languageDictionary = new Dictionary<CultureInfo, string>()
    {
        { new CultureInfo("en-US"), "English" },
        { new CultureInfo("sr-Latn-BA"), "Latinica" },
        { new CultureInfo("sr-Cyrl-BA"), "Ћирилица" },
        { new CultureInfo("hr-BA"), "Hrvatski" },
    };
    
    protected CultureInfo Culture
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
}
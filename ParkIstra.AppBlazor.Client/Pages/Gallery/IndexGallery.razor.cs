using ParkIstra.Models.Main;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Components;
using ParkIstra.AppInterfaces;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace ParkIstra.AppBlazor.Client.Pages.Gallery;
public partial class IndexGallery
{
    #region Lists
    [AllowNull]
    public List<Image> Images { get; set; }

    #endregion
    protected override async Task OnInitializedAsync()
    {
        if (Images is not null) return;

        await LoadImagesAsync();
    }

    private async Task LoadImagesAsync()
    {
        var ImagesResponse = await MainApiService.GetImagesAsync();
        if(ImagesResponse.IsSuccess)
        {
            Images = ImagesResponse.Many;
        }
    }

    [Inject, AllowNull]
    private IMainApiService MainApiService { get; set; }
    [Inject, AllowNull]
    private IWebAssemblyHostEnvironment HostEnvironment { get; set; }
}

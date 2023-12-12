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
    public List<Project> Projects { get; set; }

    #endregion
    protected override async Task OnInitializedAsync()
    {
        if (Projects is not null) return;

        await LoadProjectsAsync();
    }

    private async Task LoadProjectsAsync()
    {
        
        //TODO : remove mock
        Projects = new List<Project>
        {
            new Project { Id = 122, Name = "Project 1", Description = "Description 1"}
        };

    }

    [Inject, AllowNull]
    private IMainApiService MainApiService { get; set; }
    [Inject, AllowNull]
    private IWebAssemblyHostEnvironment HostEnvironment { get; set; }
}

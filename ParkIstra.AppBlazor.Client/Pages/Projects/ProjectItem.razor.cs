using ParkIstra.Libraries.Blazor;
using ParkIstra.Models.Main;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Components;
using ParkIstra.AppInterfaces;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace ParkIstra.AppBlazor.Client.Pages.Projects;
public partial class ProjectItem
{
    #region Lists
    [AllowNull]
    public Project Project { get; set; }

    #endregion
    protected override async Task OnInitializedAsync()
    {
        if (Project is not null) return;

        await LoadProjectAsync();
    }

    private async Task LoadProjectAsync(string? queryString = "")
    {
        //TODO : remove mock
        Project = new Project { Id = 122, Name = "Project 1", Description = "Description 1" };
    }

    [Inject, AllowNull]
    private IMainApiService MainApiService { get; set; }
    [Inject, AllowNull]
    private IWebAssemblyHostEnvironment HostEnvironment { get; set; }
}

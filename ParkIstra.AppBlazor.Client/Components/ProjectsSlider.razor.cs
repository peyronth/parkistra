using ParkIstra.Libraries.Blazor;
using ParkIstra.Models.Main;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Components;
using ParkIstra.AppInterfaces;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace ParkIstra.AppBlazor.Client.Components;
public partial class ProjectsSlider
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

    private async Task LoadProjectsAsync(string? queryString = "")
    {
        ODataQuery query = new()
        {
            ExpandList = new() { "Images" }
        };
        var ProjectsCall = await MainApiService.GetProjectsAsync(query);
        Projects = ProjectsCall.Many?.Where(p => p.Drafted).ToList();
        activeSlideIndex = Projects.FirstOrDefault()?.Id ?? 0;
    }

    [Inject, AllowNull]
    private IMainApiService MainApiService { get; set; }
    [Inject, AllowNull]
    private IWebAssemblyHostEnvironment HostEnvironment { get; set; }
}
